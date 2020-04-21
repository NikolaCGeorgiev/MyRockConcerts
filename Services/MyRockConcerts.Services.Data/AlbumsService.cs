namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Common;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services;
    using MyRockConcerts.Services.Mapping;

    public class AlbumsService : IAlbumsService
    {
        private const string ErrorMessageAlbumExist = "Тhe band already has such an album!";

        private readonly IDeletableEntityRepository<Album> albumsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public AlbumsService(
            IDeletableEntityRepository<Album> albumsRepository,
            ICloudinaryService cloudinaryService)
        {
            this.albumsRepository = albumsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(string name, IFormFile coverUrl, DateTime releaseDate, int groupId)
        {
            var album = await this.albumsRepository
                .All()
                .Where(a => a.GroupId == groupId)
                .FirstOrDefaultAsync(a => a.Name.ToUpper() == name.ToUpper());

            if (album != null)
            {
                throw new ArgumentException(ErrorMessageAlbumExist);
            }

            var url = await this.cloudinaryService.UploadPhotoAsync(
               coverUrl,
               name,
               GlobalConstants.CloudFolderForAlbumsPhotos);

            album = new Album
            {
                Name = name,
                CoverUrl = url,
                ReleaseDate = releaseDate,
                GroupId = groupId,
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
