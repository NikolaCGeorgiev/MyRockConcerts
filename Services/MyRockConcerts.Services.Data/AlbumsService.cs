namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class AlbumsService : IAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumsRepository;

        public AlbumsService(IDeletableEntityRepository<Album> albumsRepository)
        {
            this.albumsRepository = albumsRepository;
        }

        public async Task<IEnumerable<T>> GetAlbumsByGroupIdAsync<T>(int id)
        {
            var albums = this.albumsRepository.All().Where(x => x.GroupId == id);

            return await albums.To<T>().ToListAsync();
        }
    }
}
