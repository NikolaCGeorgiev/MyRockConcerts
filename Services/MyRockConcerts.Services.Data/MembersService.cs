namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class MembersService : IMembersService
    {
        private const string ErrorMessageMemeberExist = "There is a member with the same name!";

        private readonly IDeletableEntityRepository<Member> membersRepository;

        public MembersService(IDeletableEntityRepository<Member> membersRepository)
        {
            this.membersRepository = membersRepository;
        }

        public async Task<int> CreateAsync(string fullName, string imgUrl, string description, int groupId)
        {
            var member = await this.membersRepository
                .All()
                .Where(x => x.GroupId == groupId)
                .FirstOrDefaultAsync(y => y.FullName == fullName);

            if (member != null)
            {
                throw new ArgumentException(ErrorMessageMemeberExist);
            }

            var currentMember = new Member
            {
                FullName = fullName,
                ImgUrl = imgUrl,
                Description = description,
                GroupId = groupId,
            };

            await this.membersRepository.AddAsync(currentMember);
            await this.membersRepository.SaveChangesAsync();

            return currentMember.Id;
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
