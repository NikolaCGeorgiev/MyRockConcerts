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

    public class VenuesService : IVenuesService
    {
        private const string ErrorMessageNameExist = "Venue with this name alredy exist!";

        private readonly IDeletableEntityRepository<Venue> venuesRepository;

        public VenuesService(
            IDeletableEntityRepository<Venue> venuesRepository)
        {
            this.venuesRepository = venuesRepository;
        }

        public async Task<int> CreateAsync(string name, string imgUrl, string country, string city, string address, int capacity)
        {
            var venue = await this.venuesRepository
                .All()
                .FirstOrDefaultAsync(v => v.Name.ToUpper() == name.ToUpper());

            if (venue != null)
            {
                throw new ArgumentException(ErrorMessageNameExist);
            }

            venue = new Venue
            {
                Name = name,
                ImgUrl = imgUrl,
                Country = country,
                City = city,
                Address = address,
                Capacity = capacity,
            };

            await this.venuesRepository.AddAsync(venue);
            await this.venuesRepository.SaveChangesAsync();

            return venue.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var venues = this.venuesRepository.All();

            return await venues.To<T>().ToListAsync();
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
