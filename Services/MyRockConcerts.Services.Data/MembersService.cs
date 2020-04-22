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
    using MyRockConcerts.Web.ViewModels.InputModels.Members;

    public class MembersService : IMembersService
    {
        private const string ErrorMessageMemeberExist = "There is a member with the same name!";

        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly ICloudinaryService cloudinaryService;

        public MembersService(
            IDeletableEntityRepository<Member> membersRepository,
            ICloudinaryService cloudinaryService)
        {
            this.membersRepository = membersRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(string fullName, IFormFile imgUrl, string description, int groupId)
        {
            var member = await this.membersRepository
                .All()
                .Where(m => m.GroupId == groupId)
                .FirstOrDefaultAsync(m => m.FullName.ToUpper() == fullName.ToUpper());

            if (member != null)
            {
                throw new ArgumentException(ErrorMessageMemeberExist);
            }

            var url = await this.cloudinaryService.UploadPhotoAsync(
              imgUrl,
              fullName,
              GlobalConstants.CloudFolderForMembersPhotos);

            member = new Member
            {
                FullName = fullName,
                ImgUrl = url,
                Description = description,
                GroupId = groupId,
            };

            await this.membersRepository.AddAsync(member);
            await this.membersRepository.SaveChangesAsync();

            return member.Id;
        }

        public async Task<bool> EditAsync(int id, MemberEditInputModel model)
        {
            var member = await this.membersRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member.FullName.ToUpper() != model.FullName.ToUpper())
            {
                var memberWithSameName = await this.membersRepository
                    .All()
                    .FirstOrDefaultAsync(m => m.FullName == model.FullName && m.Id != id);

                if (memberWithSameName != null)
                {
                    throw new ArgumentException(ErrorMessageMemeberExist);
                }
            }

            var url = model.ImgUrl;

            if (model.Photo != null)
            {
                url = await this.cloudinaryService.UploadPhotoAsync(
                model.Photo,
                model.FullName,
                GlobalConstants.CloudFolderForMembersPhotos);
            }

            member.FullName = model.FullName;
            member.ImgUrl = url;
            member.Description = model.Description;
            member.GroupId = model.GroupId;

            this.membersRepository.Update(member);
            var result = await this.membersRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var members = this.membersRepository
                .All();

            return await members.To<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int memberId)
        {
            var member = this.membersRepository
                .All()
                .Where(m => m.Id == memberId);

            return await member.To<T>().FirstOrDefaultAsync();
        }
    }
}
