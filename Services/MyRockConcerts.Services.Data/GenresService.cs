namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private const string ErrorMessageNameExist = "Genre with this name alredy exist!";
        private const string ErrorMessageGroupHaveGenre = "The genre has already been added!";

        private readonly IRepository<GroupGenre> groupGenresRepository;
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(
            IRepository<GroupGenre> groupGenresRepository,
            IDeletableEntityRepository<Genre> genresRepository)
        {
            this.groupGenresRepository = groupGenresRepository;
            this.genresRepository = genresRepository;
        }

        public async Task<int> AddGenreAsync(int groupId, int genreId)
        {
            var groupGenre = await this.groupGenresRepository
                .All()
                .FirstOrDefaultAsync(x => x.GroupId == groupId && x.GenreId == genreId);

            if (groupGenre != null)
            {
                throw new ArgumentException(ErrorMessageGroupHaveGenre);
            }

            groupGenre = new GroupGenre
            {
                GroupId = groupId,
                GenreId = genreId,
            };

            await this.groupGenresRepository.AddAsync(groupGenre);
            await this.groupGenresRepository.SaveChangesAsync();

            return groupGenre.GroupId;
        }

        public async Task<IEnumerable<T>> AllAsync<T>()
        {
            var genres = this.genresRepository.All().OrderBy(x => x.Name);

            return await genres.To<T>().ToListAsync();
        }

        public async Task<int> CreateAsync(string name)
        {
            if (this.genresRepository.All().FirstOrDefault(x => x.Name == name) != null)
            {
                throw new ArgumentException(ErrorMessageNameExist);
            }

            var genre = new Genre
            {
                Name = name,
            };

            await this.genresRepository.AddAsync(genre);
            await this.genresRepository.SaveChangesAsync();

            return genre.Id;
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

        public async Task<int> RemoveGenreAsync(int groupId, int genreId)
        {
            var groupGenre = await this.groupGenresRepository
                .All()
                .FirstOrDefaultAsync(x => x.GroupId == groupId && x.GenreId == genreId);

            this.groupGenresRepository.Delete(groupGenre);
            await this.groupGenresRepository.SaveChangesAsync();

            return groupGenre.GroupId;
        }
    }
}
