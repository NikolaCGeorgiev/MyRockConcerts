namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupsService
    {
        Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int id);

        Task<T> GetGroupByIdAsync<T>(int id);
    }
}
