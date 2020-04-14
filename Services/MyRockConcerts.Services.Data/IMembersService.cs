namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMembersService
    {
        Task<T> GetMemberIdAsync<T>(int id);
    }
}
