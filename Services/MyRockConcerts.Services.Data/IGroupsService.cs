namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGroupsService
    {
        IQueryable<T> GetAll<T>();

        Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int id);

        Task<T> GetGroupByIdAsync<T>(int id);
    }
}
