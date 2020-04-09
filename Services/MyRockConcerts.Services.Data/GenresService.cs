namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GenresService : IGenresService
    {
        private readonly IRepository<GroupGenre> groupGenresRepository;

        public GenresService(IRepository<GroupGenre> groupGenresRepository)
        {
            this.groupGenresRepository = groupGenresRepository;
        }

        public async Task<IEnumerable<T>> GetGenresByGroupIdAsync<T>(int id)
        {
            var genres = this.groupGenresRepository.All()
                .Where(x => x.GroupId == id)
                .Select(y => y.Genre);

            return await genres.To<T>().ToListAsync();
        }
    }
}
