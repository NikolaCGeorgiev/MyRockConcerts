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

    public class AlbumsService : IAlbumsService
    {
        private const string ErrorMessageAlbumExist = "Тhe band already has such an album!";

        private readonly IDeletableEntityRepository<Album> albumsRepository;

        public AlbumsService(IDeletableEntityRepository<Album> albumsRepository)
        {
            this.albumsRepository = albumsRepository;
        }

        public async Task<int> CreateAsync(string name, string coverUrl, DateTime releaseDate, int groupId)
        {
            var album = await this.albumsRepository
                .All()
                .Where(x => x.GroupId == groupId)
                .FirstOrDefaultAsync(y => y.Name == name);

            if (album != null)
            {
                throw new ArgumentException(ErrorMessageAlbumExist);
            }

            album = new Album
            {
                Name = name,
                CoverUrl = coverUrl,
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
                .Where(x => x.GroupId == id);

            return await albums.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var albums = this.albumsRepository.All();

            return await albums.To<T>().ToListAsync();
        }
    }
}
