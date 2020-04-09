namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAlbumsService
    {
        Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id);
    }
}
