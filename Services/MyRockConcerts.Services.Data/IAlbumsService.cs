namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Web.ViewModels.InputModels.Albums;

    public interface IAlbumsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(int id);

        Task<int> CreateAsync(string name, IFormFile coverUrl, DateTime releaseDate, int groupId);

        Task<bool> EditAsync(int id, AlbumEditInputModel model);
    }
}
