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
