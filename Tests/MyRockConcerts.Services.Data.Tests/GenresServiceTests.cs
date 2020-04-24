namespace MyRockConcerts.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Data.Repositories;
    using MyRockConcerts.Web.ViewModels.Genres;
    using Xunit;

    public class GenresServiceTests
    {
        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            var actual = await genresService.CreateAsync("Power metal");
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            await genresRepository.AddAsync(new Genre
            {
                Name = "Power metal",
            });
            await genresRepository.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await genresService.CreateAsync("Power metal");
            });
        }

        [Fact]
        public async Task AddGenreAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            var actual = await genresService.AddGenreAsync(5, 1);
            var expected = 5;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddGenreAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            await groupGenresRepository.AddAsync(new GroupGenre { GroupId = 2, GenreId = 1 });
            await groupGenresRepository.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await genresService.AddGenreAsync(2, 1);
            });
        }

        [Fact]
        public async Task RemoveGenreAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            await groupGenresRepository.AddAsync(new GroupGenre { GroupId = 2, GenreId = 1 });
            await groupGenresRepository.SaveChangesAsync();

            var actual = await genresService.RemoveGenreAsync(2, 1);
            var expected = 2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(1, 5)]
        public async Task RemoveGenreAsyncWithInCorrectDataShoulThrowNullReferenceException(int groupId, int genreId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await genresService.RemoveGenreAsync(groupId, genreId);
            });
        }

        [Fact]
        public async Task GetNameByIdAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var genresRepository = new EfDeletableEntityRepository<Genre>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var genresService = new GenresService(groupGenresRepository, genresRepository);

            await genresRepository.AddAsync(new Genre { Name = "Power metal" });
            await genresRepository.SaveChangesAsync();

            var actual = await genresService.GetNameByIdAsync(1);
            var expected = "Power metal";

            Assert.Equal(expected, actual);
        }
    }
}
