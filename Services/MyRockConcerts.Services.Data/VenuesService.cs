namespace MyRockConcerts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class VenuesService : IVenuesService
    {
        private readonly IDeletableEntityRepository<Venue> venuesRepository;

        public VenuesService(IDeletableEntityRepository<Venue> venuesRepository)
        {
            this.venuesRepository = venuesRepository;
        }

        public async Task<T> GetByIdAsync<T>(int venueId)
        {
            var venue = this.venuesRepository
                .All()
                .Where(v => v.Id == venueId);

            return await venue.To<T>().FirstOrDefaultAsync();
        }
    }
}
