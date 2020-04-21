namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IMembersService
    {
        Task<T> GetByIdAsync<T>(int memberId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string fullName, IFormFile imgUrl, string description, int groupId);
    }
}
