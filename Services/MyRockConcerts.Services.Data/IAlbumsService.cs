namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAlbumsService
    {
        Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string name, string coverUrl, DateTime releaseDate, int groupId);
    }
}
