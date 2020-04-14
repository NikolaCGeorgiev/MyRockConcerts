namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGroupsService
    {
        IQueryable<T> GetAll<T>();

        Task<T> GetGroupByIdAsync<T>(int id);

        IQueryable<T> GetGroupsByGenreId<T>(int genreId);

        Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int concertId);

        Task<bool> IsMyFavoriteAsync(int groupId, string userId);

        Task AddToMyFavoritesAsync(int groupId, string userId);

        Task RemoveFromMyFavoritesAsync(int groupId, string userId);
    }
}
