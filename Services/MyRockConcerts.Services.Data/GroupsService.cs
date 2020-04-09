namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupsService : IGroupsService
    {
        private readonly IRepository<ConcertGroup> concertGroupsRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;

        public GroupsService(
            IRepository<ConcertGroup> concertGroupsRepository,
            IDeletableEntityRepository<Group> groupsRepository)
        {
            this.concertGroupsRepository = concertGroupsRepository;
            this.groupsRepository = groupsRepository;
        }

        public async Task<T> GetGroupByIdAsync<T>(int id)
        {
            var group = this.groupsRepository.All().Where(x => x.Id == id);

            return await group.To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int id)
        {
            var concerts = this.concertGroupsRepository.All()
                .Where(x => x.ConcertId == id)
                .Select(y => y.Group);

            return await concerts.To<T>().ToListAsync();
        }
    }
}
