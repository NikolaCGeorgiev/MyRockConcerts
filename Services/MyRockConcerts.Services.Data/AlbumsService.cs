namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.InputModels.Albums;

    public class AlbumsService : IAlbumsService
    {
        private const string ErrorMessageAlbumExist = "Тhe band already has such an album!";

        private readonly IDeletableEntityRepository<Album> albumsRepository;

        public AlbumsService(IDeletableEntityRepository<Album> albumsRepository)
        {
            this.albumsRepository = albumsRepository;
        }

        public async Task<int> CreateAsync(AlbumServiceModel model)
        {
            var album = await this.albumsRepository
                .All()
                .Where(a => a.GroupId == model.GroupId)
                .FirstOrDefaultAsync(a => a.Name.ToUpper() == model.Name.ToUpper());

            if (album != null)
            {
                throw new ArgumentException(ErrorMessageAlbumExist);
            }

            album = new Album
            {
                Name = model.Name,
                CoverUrl = model.CoverUrl,
                ReleaseDate = model.ReleaseDate,
                GroupId = model.GroupId,
            };

            await this.albumsRepository.AddAsync(album);
            await this.albumsRepository.SaveChangesAsync();

            return album.Id;
        }

        public async Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id)
        {
            var albums = this.albumsRepository
                .All()
                .Where(a => a.GroupId == id);

            return await albums.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var albums = this.albumsRepository.All();

            return await albums.To<T>().ToListAsync();
        }
    }
}
