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
                .FirstOrDefaultAsync(gg => gg.GroupId == groupId && gg.GenreId == genreId);

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
            var genres = this.genresRepository.All().OrderBy(g => g.Name);

            return await genres.To<T>().ToListAsync();
        }

        public async Task<int> CreateAsync(string name)
        {
            var genre = this.genresRepository
                .All()
                .FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());

            if (genre != null)
            {
                throw new ArgumentException(ErrorMessageNameExist);
            }

            genre = new Genre
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
                .Where(gg => gg.GroupId == id)
                .Select(gg => gg.Genre);

            return await genres.To<T>().ToListAsync();
        }

        public async Task<string> GetNameByIdAsync(int genreId)
        {
            var name = this.genresRepository.All().Where(g => g.Id == genreId)
                .Select(g => g.Name).FirstOrDefaultAsync();

            return await name;
        }

        public async Task<int> RemoveGenreAsync(int groupId, int genreId)
        {
            var groupGenre = await this.groupGenresRepository
                .All()
                .FirstOrDefaultAsync(gg => gg.GroupId == groupId && gg.GenreId == genreId);

            this.groupGenresRepository.Delete(groupGenre);
            await this.groupGenresRepository.SaveChangesAsync();

            return groupGenre.GroupId;
        }
    }
}
