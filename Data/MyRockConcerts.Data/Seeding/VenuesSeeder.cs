namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class VenuesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Venues.Any())
            {
                return;
            }

            await dbContext.Venues.AddAsync(new Venue
            {
                Name = "Port Varna",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377702/venues_photos/Port_Varna_t44ryy.jpg",
                Country = "Bulgaria",
                City = "Varna",
                Address = "Sq. Slaveykov 1",
                Capacity = 30000,
            });

            await dbContext.Venues.AddAsync(new Venue
            {
                Name = "Rowing Canal",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377745/venues_photos/Rowing_Canal_g5uvuq.jpg",
                Country = "Bulgaria",
                City = "Plovdiv",
                Address = "Yasna Polyana",
                Capacity = 70000,
            });

            await dbContext.Venues.AddAsync(new Venue
            {
                Name = "Arena Armeec",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377791/venues_photos/Arena_Armeec_ocuezc.jpg",
                Country = "Bulgaria",
                City = "Sofia",
                Address = "Asen Yordanov 1",
                Capacity = 45000,
            });
        }
    }
}
