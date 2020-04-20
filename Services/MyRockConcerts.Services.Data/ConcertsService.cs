namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class ConcertsService : IConcertsService
    {
        private const string ErrorMessageConcertExist = "Concert with this name already exist!";
        private const string ErrorMessageDate = "Мust be an upcoming date!";
        private const string ErrorMessageDateTimeFormat = "Date must bi in format yyyy-MM-dd HH:mm";

        private readonly IDeletableEntityRepository<Concert> concertsRepository;
        private readonly IRepository<UserConcert> userConcertRepository;
        private readonly IRepository<ConcertGroup> concertsGroupsRepository;

        public ConcertsService(
            IDeletableEntityRepository<Concert> concertsRepository,
            IRepository<UserConcert> userConcertRepository,
            IRepository<ConcertGroup> concertsGroupsRepository)
        {
            this.concertsRepository = concertsRepository;
            this.userConcertRepository = userConcertRepository;
            this.concertsGroupsRepository = concertsGroupsRepository;
        }

        public async Task<bool> IsInMyConcertsAsync(int concertId, string userId)
        {
            var userConcerts = await this.userConcertRepository
                .All()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConcertId == concertId);

            if (userConcerts != null)
            {
                return true;
            }

            return false;
        }

        public IQueryable<T> GetAllUpcoming<T>(int? count = null)
        {
            var filterDate = DateTime.UtcNow;

            IQueryable<Concert> query = this.concertsRepository
                .All()
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var concert = this.concertsRepository
                .All()
                .Where(c => c.Id == id);

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

        public IQueryable<T> GetMyConcerts<T>(string userId)
        {
            var filterDate = DateTime.UtcNow;

            var concerts = this.userConcertRepository
                .All()
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Concert)
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            return concerts.To<T>();
        }

        public async Task RemoveFromMyConcertsAsync(int concertId, string userId)
        {
            var userConcert = new UserConcert
            {
                UserId = userId,
                ConcertId = concertId,
            };

            this.userConcertRepository.Delete(userConcert);
            await this.userConcertRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetByGroupsId<T>(int groupId)
        {
            var filterDate = DateTime.UtcNow;

            var concerts = this.concertsGroupsRepository
                .All()
                .Where(cg => cg.GroupId == groupId)
                .Select(cg => cg.Concert)
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            return concerts.To<T>();
        }

        public async Task<int> CreateAsync(string name, string imgUrl, DateTime date, string ticketUrl, int venueId)
        {
            var isValidDateFormat = DateTime.TryParseExact(date.ToString(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

            if (!isValidDateFormat)
            {
                throw new ArgumentException(ErrorMessageDateTimeFormat);
            }

            var concert = await this.concertsRepository
                .All()
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync(y => y.Name.ToUpper() == name.ToUpper());

            if (concert != null)
            {
                throw new ArgumentException(ErrorMessageConcertExist);
            }

            var filterDate = DateTime.UtcNow;

            if (date < filterDate)
            {
                throw new ArgumentException(ErrorMessageDate);
            }

            concert = new Concert
            {
                Name = name,
                ImgUrl = imgUrl,
                Date = result,
                TicketUrl = ticketUrl,
                VenueId = venueId,
            };

            await this.concertsRepository.AddAsync(concert);
            await this.concertsRepository.SaveChangesAsync();

            return concert.Id;
        }
    }
}
