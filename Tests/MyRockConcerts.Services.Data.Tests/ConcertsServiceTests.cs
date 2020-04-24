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
    using MyRockConcerts.Web.ViewModels.InputModels.Concerts;
    using Xunit;

    public class ConcertsServiceTests
    {
        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var date = DateTime.ParseExact("2020-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            var actual = await concertsService
                .CreateAsync("Hills Of Rock 2020", photo.Object, date, "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html", 2);
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            await concertsRepository.AddAsync(new Concert
            {
                Name = "Hills Of Rock 2020",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376364/concerts_photos/Hills_Of_Rock_2020_rcutks.jpg",
                Date = DateTime.ParseExact("2020-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html",
                VenueId = 2,
            });
            await concertsRepository.SaveChangesAsync();

            var date = DateTime.ParseExact("2020-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await concertsService.CreateAsync("Hills Of Rock 2020", photo.Object, date, "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html", 2);
            });
        }

        [Fact]
        public async Task CreateAsyncWithPreviousDateShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var date = DateTime.ParseExact("2019-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await concertsService.CreateAsync("Hills Of Rock 2020", photo.Object, date, "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html", 2);
            });
        }

        [Fact]
        public async Task EditAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var date = DateTime.ParseExact("2020-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            var id = await concertsService.CreateAsync("Hills Of Rock 2020", photo.Object, date, "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html", 2);

            var concert = new ConcertEditInputModel
            {
                Name = "Varna Rock 2020",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg",
                Date = DateTime.ParseExact("2020-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            };

            Assert.True(await concertsService.EditAsync(id, concert));
        }

        [Fact]
        public async Task EditeAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            await concertsRepository.AddAsync(new Concert
            {
                Name = "Hills Of Rock 2020",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376364/concerts_photos/Hills_Of_Rock_2020_rcutks.jpg",
                Date = DateTime.ParseExact("2020-07-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html",
                VenueId = 2,
            });
            await concertsRepository.SaveChangesAsync();

            var date = DateTime.ParseExact("2020-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            var id = await concertsService.CreateAsync("Varna Rock 2020", photo.Object, date, "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg", 3);

            var concert = new ConcertEditInputModel
            {
                Name = "Hills Of Rock 2020",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg",
                Date = DateTime.ParseExact("2020-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await concertsService.EditAsync(id, concert);
            });
        }

        [Fact]
        public async Task EditeAsyncWithPreviousDateShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertsRepository = new EfDeletableEntityRepository<Concert>(dbContext);
            var userConcertsRepository = new EfRepository<UserConcert>(dbContext);
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var concertsService = new ConcertsService(concertsRepository, userConcertsRepository, concertGroupsRepository, cloudinary.Object);
            var photo = new Mock<IFormFile>();

            var date = DateTime.ParseExact("2020-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            var id = await concertsService.CreateAsync("Varna Rock 2020", photo.Object, date, "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg", 3);

            var concert = new ConcertEditInputModel
            {
                Name = "Varna Rock 2020",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg",
                Date = DateTime.ParseExact("2019-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await concertsService.EditAsync(id, concert);
            });
        }
    }
}
