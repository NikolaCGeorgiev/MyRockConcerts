namespace MyRockConcerts.Services.Data.Tests
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyRockConcerts.Data;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Data.Repositories;
    using MyRockConcerts.Web.ViewModels.InputModels.Albums;
    using Xunit;

    public class AlbumsServiceTests
    {
        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var albumsRepository = new EfDeletableEntityRepository<Album>(dbContext);
            var albumsService = new AlbumsService(albumsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var releaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var actual = await albumsService.CreateAsync("Carolus Rex", photo.Object, releaseDate, 4);
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsyncWithInCorrectDataShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var albumsRepository = new EfDeletableEntityRepository<Album>(dbContext);
            var albumsService = new AlbumsService(albumsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var releaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            await albumsRepository.AddAsync(new Album
            {
                Name = "Carolus Rex",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375890/albums_photos/Carolus_Rex_ftub8r.jpg",
                ReleaseDate = releaseDate,
                GroupId = 4,
            });
            await albumsRepository.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await albumsService.CreateAsync("Carolus Rex", photo.Object, releaseDate, 4);
            });
        }

        [Fact]
        public async Task EditAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var albumsRepository = new EfDeletableEntityRepository<Album>(dbContext);
            var albumsService = new AlbumsService(albumsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var releaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var id = await albumsService.CreateAsync("Carolus Rex", photo.Object, releaseDate, 4);

            var album = new AlbumEditInputModel
            {
                Name = "Primo Victoria",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375808/albums_photos/Primo_Victoria_xhi3ny.jpg",
                ReleaseDate = DateTime.ParseExact("2005-03-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            };

            Assert.True(await albumsService.EditAsync(id, album));
        }

        [Fact]
        public async Task EditeAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var albumsRepository = new EfDeletableEntityRepository<Album>(dbContext);
            var albumsService = new AlbumsService(albumsRepository, cloudinary.Object);

            await albumsRepository.AddAsync(new Album
            {
                Name = "Primo Victoria",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375808/albums_photos/Primo_Victoria_xhi3ny.jpg",
                ReleaseDate = DateTime.ParseExact("2005-03-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            });
            await albumsRepository.SaveChangesAsync();

            var photo = new Mock<IFormFile>();
            var releaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var id = await albumsService.CreateAsync("Carolus Rex", photo.Object, releaseDate, 4);

            var album = new AlbumEditInputModel
            {
                Name = "Primo Victoria",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375808/albums_photos/Primo_Victoria_xhi3ny.jpg",
                ReleaseDate = DateTime.ParseExact("2005-03-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await albumsService.EditAsync(id, album);
            });
        }
    }
}
