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
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(
            IRepository<GroupGenre> groupGenresRepository,
            IDeletableEntityRepository<Genre> genresRepository)
        {
            this.groupGenresRepository = groupGenresRepository;
            this.genresRepository = genresRepository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>()
        {
            var genres = this.genresRepository.All().OrderBy(x => x.Name);

            return await genres.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetGenresByGroupIdAsync<T>(int id)
        {
            var genres = this.groupGenresRepository.All()
                .Where(x => x.GroupId == id)
                .Select(y => y.Genre);

            return await genres.To<T>().ToListAsync();
        }

        public async Task<string> GetNameByIdAsync(int genreId)
        {
            var name = this.genresRepository.All().Where(x => x.Id == genreId)
                .Select(x => x.Name).FirstOrDefaultAsync();

            return await name;
        }
    }
}
