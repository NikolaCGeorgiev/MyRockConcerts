namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class ConcertsService : IConcertsService
    {
        private readonly IDeletableEntityRepository<Concert> concertsRepository;
        private readonly IRepository<UserConcert> userConcertRepository;

        public ConcertsService(
            IDeletableEntityRepository<Concert> concertsRepository,
            IRepository<UserConcert> userConcertRepository)
        {
            this.concertsRepository = concertsRepository;
            this.userConcertRepository = userConcertRepository;
        }

        public async Task<bool> IsInMyConcertsAsync(int concertId, string userId)
        {
            var concert = await this.userConcertRepository.All().FirstOrDefaultAsync(x => x.UserId == userId && x.ConcertId == concertId);

            if (concert != null)
            {
                return true;
            }

            return false;
        }

        public IQueryable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Concert> query =
                this.concertsRepository.All().OrderBy(x => x.Date);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var concert = this.concertsRepository.All().Where(x => x.Id == id);

            return await concert.To<T>().FirstOrDefaultAsync();
        }

        public async Task AddToMyConcertsAsync(int concertId, string userId)
        {
            if (await this.IsInMyConcertsAsync(concertId, userId))
            {
                return;
            }

            var userConcert = new UserConcert
            {
                UserId = userId,
                ConcertId = concertId,
            };

            await this.userConcertRepository.AddAsync(userConcert);
            await this.userConcertRepository.SaveChangesAsync();
        }
    }
}
