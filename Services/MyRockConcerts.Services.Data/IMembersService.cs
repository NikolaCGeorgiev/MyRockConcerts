namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMembersService
    {
        Task<T> GetByIdAsync<T>(int memberId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string fullName, string imgUrl, string description, int groupId);
    }
}
