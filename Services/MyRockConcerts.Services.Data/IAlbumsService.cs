namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyRockConcerts.Web.ViewModels.InputModels.Albums;

    public interface IAlbumsService
    {
        Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(AlbumServiceModel model);
    }
}
