namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IAlbumsService
    {
        Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string name, IFormFile coverUrl, DateTime releaseDate, int groupId);
    }
}
