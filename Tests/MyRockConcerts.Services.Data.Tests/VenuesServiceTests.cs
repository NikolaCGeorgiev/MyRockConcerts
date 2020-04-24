namespace MyRockConcerts.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyRockConcerts.Data;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Data.Repositories;
    using MyRockConcerts.Web.ViewModels.InputModels.Venues;
    using Xunit;

    public class VenuesServiceTests
    {
        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var venuesRepository = new EfDeletableEntityRepository<Venue>(dbContext);
            var venuesService = new VenuesService(venuesRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var actual = await venuesService
                .CreateAsync("Port Varna", photo.Object, "Bulgaria", "Varna", "Sq. Slaveykov 1", 30000);
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
            var venuesRepository = new EfDeletableEntityRepository<Venue>(dbContext);
            var venuesService = new VenuesService(venuesRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            await venuesRepository.AddAsync(new Venue
            {
                Name = "Port Varna",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377702/venues_photos/Port_Varna_t44ryy.jpg",
                Country = "Bulgaria",
                City = "Varna",
                Address = "Sq. Slaveykov 1",
                Capacity = 30000,
            });
            await venuesRepository.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await venuesService.CreateAsync("Port Varna", photo.Object, "Bulgaria", "Sofia", "Sq. Slaveykov 1", 100000);
            });
        }

        [Fact]
        public async Task EditAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var venuesRepository = new EfDeletableEntityRepository<Venue>(dbContext);
            var venuesService = new VenuesService(venuesRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var id = await venuesService.CreateAsync("Rowing Canal", photo.Object, "Bulgaria", "Plovdiv", "Yasna Polyana", 70000);

            var venue = new VenueEditInputModel
            {
                Name = "Port Varna",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377702/venues_photos/Port_Varna_t44ryy.jpg",
                Country = "Bulgaria",
                City = "Varna",
                Address = "Sq. Slaveykov 1",
                Capacity = 30000,
            };

            Assert.True(await venuesService.EditAsync(id, venue));
        }

        [Fact]
        public async Task EditeAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var venuesRepository = new EfDeletableEntityRepository<Venue>(dbContext);
            var venuesService = new VenuesService(venuesRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            await venuesRepository.AddAsync(new Venue
            {
                Name = "Port Varna",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377702/venues_photos/Port_Varna_t44ryy.jpg",
                Country = "Bulgaria",
                City = "Varna",
                Address = "Sq. Slaveykov 1",
                Capacity = 30000,
            });
            await venuesRepository.SaveChangesAsync();

            var id = await venuesService.CreateAsync("Rowing Canal", photo.Object, "Bulgaria", "Plovdiv", "Yasna Polyana", 70000);

            var venue = new VenueEditInputModel
            {
                Name = "Port Varna",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377745/venues_photos/Rowing_Canal_g5uvuq.jpg",
                Country = "Bulgaria",
                City = "Plovdiv",
                Address = "Yasna Polyana",
                Capacity = 70000,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await venuesService.EditAsync(id, venue);
            });
        }
    }
}
