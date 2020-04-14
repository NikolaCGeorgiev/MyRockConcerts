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
        private readonly IRepository<GroupGenre> groupGenresRepository;

        public GroupsService(
            IRepository<ConcertGroup> concertGroupsRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IRepository<GroupGenre> groupGenresRepository)
        {
            this.concertGroupsRepository = concertGroupsRepository;
            this.groupsRepository = groupsRepository;
            this.groupGenresRepository = groupGenresRepository;
        }

        public IQueryable<T> GetAll<T>()
        {
            var groups = this.groupsRepository
                .All()
                .OrderBy(g => g.Name);

            return groups.To<T>();
        }

        public async Task<T> GetGroupByIdAsync<T>(int id)
        {
            var group = this.groupsRepository
                .All()
                .Where(g => g.Id == id);

            return await group.To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int concertId)
        {
            var concerts = this.concertGroupsRepository
                .All()
                .Where(cg => cg.ConcertId == concertId)
                .Select(cg => cg.Group);

            return await concerts.To<T>().ToListAsync();
        }

        public IQueryable<T> GetGroupsByGenreId<T>(int genreId)
        {
            var groups = this.groupGenresRepository
                .All()
                .Where(gg => gg.GenreId == genreId)
                .Select(gg => gg.Group)
                .OrderBy(g => g.Name);

            return groups.To<T>();
        }
    }
}
