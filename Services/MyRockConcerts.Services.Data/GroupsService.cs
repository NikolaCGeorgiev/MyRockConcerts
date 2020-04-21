namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Common;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.InputModels.Groups;

    public class GroupsService : IGroupsService
    {
        private const string ErrorMessageNameExist = "Group with this name alredy exist!";
        private const string ErrorMessageAlreadyAdded = "The group has already been added!";

        private readonly IRepository<ConcertGroup> concertGroupsRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IRepository<GroupGenre> groupGenresRepository;
        private readonly IRepository<UserGroup> userGroupsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public GroupsService(
            IRepository<ConcertGroup> concertGroupsRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IRepository<GroupGenre> groupGenresRepository,
            IRepository<UserGroup> userGroupsRepository,
            ICloudinaryService cloudinaryService)
        {
            this.concertGroupsRepository = concertGroupsRepository;
            this.groupsRepository = groupsRepository;
            this.groupGenresRepository = groupGenresRepository;
            this.userGroupsRepository = userGroupsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> AddGroupAsync(int concertId, int groupId)
        {
            var concertGroup = await this.concertGroupsRepository
                .All()
                .FirstOrDefaultAsync(cg => cg.ConcertId == concertId && cg.GroupId == groupId);

            if (concertGroup != null)
            {
                throw new ArgumentException(ErrorMessageAlreadyAdded);
            }

            concertGroup = new ConcertGroup
            {
                ConcertId = concertId,
                GroupId = groupId,
            };

            await this.concertGroupsRepository.AddAsync(concertGroup);
            await this.groupGenresRepository.SaveChangesAsync();

            return concertGroup.ConcertId;
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

        public async Task<int> CreateAsync(string name, IFormFile imgUrl, string description)
        {
            var group = this.groupsRepository
                .All()
                .FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());

            if (group != null)
            {
                throw new ArgumentException(ErrorMessageNameExist);
            }

            var url = await this.cloudinaryService.UploadPhotoAsync(
                imgUrl,
                name,
                GlobalConstants.CloudFolderForGroupsPhotos);

            group = new Group
            {
                Name = name,
                ImgUrl = url,
                Description = description,
            };

            await this.groupsRepository.AddAsync(group);
            await this.groupsRepository.SaveChangesAsync();

            return group.Id;
        }

        public async Task<bool> EditAsync(int id, GroupEditInputModel model)
        {
            var group = await this.groupsRepository
                .All()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group.Name.ToUpper() != model.Name.ToUpper())
            {
                var groupWithSameName = await this.groupsRepository
                .All()
                .FirstOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper());

                if (groupWithSameName != null)
                {
                    throw new ArgumentException(ErrorMessageNameExist);
                }
            }

            var url = model.ImgUrl;

            if (model.Photo != null)
            {
                url = await this.cloudinaryService.UploadPhotoAsync(
                model.Photo,
                model.Name,
                GlobalConstants.CloudFolderForGroupsPhotos);
            }

            group.Name = model.Name;
            group.ImgUrl = model.ImgUrl;
            group.Description = model.Description;

            this.groupsRepository.Update(group);
            var result = await this.groupsRepository.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<T> GetAll<T>(string userId = null)
        {
            var groups = this.groupsRepository
                .All();

            if (userId != null)
            {
                groups = this.userGroupsRepository
                    .All()
                    .Where(ug => ug.UserId == userId)
                    .Select(ug => ug.Group);
            }

            return groups.OrderBy(g => g.Name).To<T>();
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

        public async Task<int> RemoveGroupAsync(int concertId, int groupId)
        {
            var concertGroup = await this.concertGroupsRepository
                .All()
                .FirstOrDefaultAsync(cg => cg.ConcertId == concertId && cg.GroupId == groupId);

            this.concertGroupsRepository.Delete(concertGroup);
            await this.concertGroupsRepository.SaveChangesAsync();

            return concertGroup.ConcertId;
        }
    }
}
