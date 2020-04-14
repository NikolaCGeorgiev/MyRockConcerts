namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGroupsService
    {
        IQueryable<T> GetAll<T>();

        IQueryable<T> GetGroupsByGenreId<T>(int genreId);

        Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int concertId);

        Task<T> GetGroupByIdAsync<T>(int id);
    }
}
