namespace MyRockConcerts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class MembersService : IMembersService
    {
        private readonly IDeletableEntityRepository<Member> membersRepository;

        public MembersService(IDeletableEntityRepository<Member> membersRepository)
        {
            this.membersRepository = membersRepository;
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
