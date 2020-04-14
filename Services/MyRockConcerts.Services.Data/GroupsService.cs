﻿namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupsService : IGroupsService
    {
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

        public IQueryable<T> GetAll<T>()
        {
            var groups = this.groupsRepository
                .All()
                .OrderBy(g => g.Name);

            return groups.To<T>();
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
