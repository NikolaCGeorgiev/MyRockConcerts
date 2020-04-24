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
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.InputModels.Groups;
    using Xunit;

    public class GroupsServiceTests
    {
        [Fact]
        public async Task AddGroupAsyncWithCorrectDataShouldAddGroupToConcert()
        {
            MapperInitializer.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            await groupService.AddGroupAsync(1, 1);

            Assert.Equal(1, await concertGroupsRepository.All().CountAsync());
        }

        [Fact]
        public async Task AddGroupAsyncWithInCorrectDataShouldThrowArgumentException()
        {
            MapperInitializer.InitializeMapper();
            var groupService = this.GroupsService();

            await groupService.AddGroupAsync(1, 1);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await groupService.AddGroupAsync(1, 1);
            });
        }

        [Fact]
        public async Task GetGroupByIdAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            await groupsRepository.AddAsync(new Group
            {
                Id = 1,
                Name = "Sabaton",
                ImgUrl = "url",
                Description = "description",
            });
            await groupsRepository.SaveChangesAsync();

            var group = await groupService.GetGroupByIdAsync<GroupTestVewModel>(1);
            var actual = group.Name;
            var expected = "Sabaton";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();
            var actual = await groupService.CreateAsync("Sabaton", photo.Object, "description");
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
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();

            await groupService.CreateAsync("Sabaton", photo.Object, "description");

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await groupService.CreateAsync("Sabaton", photo.Object, "description");
            });
        }

        [Fact]
        public async Task EditAsyncWithCorrectDataShouldReturnCorrectResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();

            var id = await groupService.CreateAsync("Sabaton", photo.Object, "description");

            var model = new GroupEditInputModel
            {
                Name = "Sabaton",
                Photo = photo.Object,
                Description = "other",
            };

            Assert.True(await groupService.EditAsync(id, model));
        }

        [Fact]
        public async Task EditeAsyncWithDublicateNameShouldThrowArgumentException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            var photo = new Mock<IFormFile>();

            await groupService.CreateAsync("Sabaton", photo.Object, "description");
            var id = await groupService.CreateAsync("Nightwish", photo.Object, "description");

            var model = new GroupEditInputModel
            {
                Name = "Sabaton",
                Photo = photo.Object,
                Description = "description",
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await groupService.EditAsync(id, model);
            });
        }

        [Fact]
        public async Task RemoveGroupAsyncWithCorrectDataShouldRemoveGroupFromConcert()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            await concertGroupsRepository.AddAsync(new ConcertGroup { ConcertId = 5, GroupId = 1 });
            await concertGroupsRepository.SaveChangesAsync();

            var actual = await groupService.RemoveGroupAsync(5, 1);
            var expectedId = 5;

            Assert.Equal(expectedId, actual);
        }

        [Fact]
        public async Task RemoveGroupAsyncWithInCorrectDataShouldThrowArgumentException()
        {
            MapperInitializer.InitializeMapper();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await groupService.RemoveGroupAsync(5, 1);
            });
        }

        private GroupsService GroupsService()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var cloudinary = new Mock<ICloudinaryService>();
            var concertGroupsRepository = new EfRepository<ConcertGroup>(dbContext);
            var groupsRepository = new EfDeletableEntityRepository<Group>(dbContext);
            var groupGenresRepository = new EfRepository<GroupGenre>(dbContext);
            var usertGroupsRepository = new EfRepository<UserGroup>(dbContext);
            var groupService = new GroupsService(concertGroupsRepository, groupsRepository, groupGenresRepository, usertGroupsRepository, cloudinary.Object);

            return groupService;
        }
    }
}
