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
    using MyRockConcerts.Services.Data.Tests.Common;
    using MyRockConcerts.Web.ViewModels.InputModels.Members;
    using MyRockConcerts.Web.ViewModels.Members;
    using Xunit;

    public class MembersServiceTests
    {
        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            var service = new MembersService(membersRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();
            var actual = await service.CreateAsync("Kirk Hammett", photo.Object, "description", 2);
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            var service = new MembersService(membersRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();

            await service.CreateAsync("Lars Ulrich", photo.Object, "description", 2);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.CreateAsync("Lars Ulrich", photo.Object, "description", 2);
            });
        }

        [Fact]
        public async Task EditAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            var service = new MembersService(membersRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();
            var id = await service.CreateAsync("Kirk Hammett", photo.Object, "description", 2);

            var model = new MemberEditInputModel
            {
                FullName = "Kirk Hammett",
                Photo = photo.Object,
                Description = "Other",
                GroupId = 1,
            };

            Assert.True(await service.EditAsync(id, model));
        }

        [Fact]
        public async Task EditeAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            var service = new MembersService(membersRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();

            await service.CreateAsync("Lars Ulrich", photo.Object, "description", 2);
            var id = await service.CreateAsync("Kirk Hammett", photo.Object, "description", 2);

            var model = new MemberEditInputModel
            {
                FullName = "Lars Ulrich",
                Photo = photo.Object,
                Description = "description",
                GroupId = 2,
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.EditAsync(id, model);
            });
        }

        [Fact]
        public async Task GetByIdAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var membersRepository = new EfDeletableEntityRepository<Member>(dbContext);
            var service = new MembersService(membersRepository, cloudinary.Object);

            await membersRepository.AddAsync(new Member
            {
                Id = 1,
                FullName = "James Hetfield",
                ImgUrl = "url",
                Description = "description",
                GroupId = 1,
            });
            await membersRepository.SaveChangesAsync();

            var member = await service.GetByIdAsync<MemberTestVewModel>(1);
            var actual = member.FullName;
            var expected = "James Hetfield";

            Assert.Equal(expected, actual);
        }
    }
}
