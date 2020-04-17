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

    public class GroupsService : IGroupsService
    {
        private const string ErrorMessageNameExist = "Group with this name alredy exist!";

        private readonly IRepository<ConcertGroup> concertGroupsRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IRepository<GroupGenre> groupGenresRepository;
        private readonly IRepository<UserGroup> userGroupsRepository;

        public GroupsService(
            IRepository<ConcertGroup> concertGroupsRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IRepository<GroupGenre> groupGenresRepository,
            IRepository<UserGroup> userGroupsRepository)
        {
            this.concertGroupsRepository = concertGroupsRepository;
            this.groupsRepository = groupsRepository;
            this.groupGenresRepository = groupGenresRepository;
            this.userGroupsRepository = userGroupsRepository;
        }

        public async Task AddToMyFavoritesAsync(int groupId, string userId)
        {
            if (await this.IsMyFavoriteAsync(groupId, userId))
            {
                return;
            }

            var userGroup = new UserGroup
            {
                UserId = userId,
                GroupId = groupId,
            };

            await this.userGroupsRepository.AddAsync(userGroup);
            await this.userGroupsRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(string name, string imgUrl, string description)
        {
            if (this.groupsRepository.All().FirstOrDefault(x => x.Name == name) != null)
            {
                throw new ArgumentException(ErrorMessageNameExist);
            }

            var group = new Group
            {
                Name = name,
                ImgUrl = imgUrl,
                Description = description,
            };

            await this.groupsRepository.AddAsync(group);
            await this.groupsRepository.SaveChangesAsync();

            return group.Id;
        }

        public IQueryable<T> GetAll<T>(string userId = null)
        {
            var groups = this.groupsRepository
                .All();

            if (userId != null)
            {
                groups = this.userGroupsRepository.All().Where(x => x.UserId == userId).Select(x => x.Group);
            }

            return groups.OrderBy(x => x.Name).To<T>();
        }

        public async Task<T> GetGroupByIdAsync<T>(int id)
        {
            var group = this.groupsRepository
                .All()
                .Where(g => g.Id == id);

            return await group.To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int concertId)
        {
            var concerts = this.concertGroupsRepository
                .All()
                .Where(cg => cg.ConcertId == concertId)
                .Select(cg => cg.Group);

            return await concerts.To<T>().ToListAsync();
        }

        public IQueryable<T> GetGroupsByGenreId<T>(int genreId)
        {
            var groups = this.groupGenresRepository
                .All()
                .Where(gg => gg.GenreId == genreId)
                .Select(gg => gg.Group)
                .OrderBy(g => g.Name);

            return groups.To<T>();
        }

        public async Task<bool> IsMyFavoriteAsync(int groupId, string userId)
        {
            var userGroups = await this.userGroupsRepository
                .All()
                .FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId);

            if (userGroups != null)
            {
                return true;
            }

            return false;
        }

        public async Task RemoveFromMyFavoritesAsync(int groupId, string userId)
        {
            var userGroup = new UserGroup
            {
                UserId = userId,
                GroupId = groupId,
            };

            this.userGroupsRepository.Delete(userGroup);
            await this.userGroupsRepository.SaveChangesAsync();
        }
    }
}
