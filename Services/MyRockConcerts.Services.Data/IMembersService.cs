namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMembersService
    {
        Task<IEnumerable<T>> GetMembersByGroupIdAsync<T>(int id);
    }
}
