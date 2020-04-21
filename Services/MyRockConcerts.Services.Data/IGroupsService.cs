namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Web.ViewModels.InputModels.Groups;

    public interface IGroupsService
    {
        IQueryable<T> GetAll<T>(string userId = null);

        Task<T> GetGroupByIdAsync<T>(int id);

        IQueryable<T> GetGroupsByGenreId<T>(int genreId);

        Task<IEnumerable<T>> GetGroupsByConcertIdAsync<T>(int concertId);

        Task<bool> IsMyFavoriteAsync(int groupId, string userId);

        Task AddToMyFavoritesAsync(int groupId, string userId);

        Task RemoveFromMyFavoritesAsync(int groupId, string userId);

        Task<int> CreateAsync(string name, IFormFile imgUrl, string description);

        Task<int> AddGroupAsync(int concertId, int groupId);

        Task<int> RemoveGroupAsync(int concertId, int groupId);
    }
}
