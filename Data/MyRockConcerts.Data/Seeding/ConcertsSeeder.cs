namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class ConcertsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Concerts.Any())
            {
                return;
            }

            await dbContext.Concerts.AddAsync(new Concert
            {
                Name = "Hills Of Rock 2021",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376364/concerts_photos/Hills_Of_Rock_2020_rcutks.jpg",
                Date = DateTime.ParseExact("2021-10-24 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html",
                VenueId = 2,
            });

            await dbContext.Concerts.AddAsync(new Concert
            {
                Name = "Varna Rock 2021",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376430/concerts_photos/Varna_Rock_2020_gwgfri.jpg",
                Date = DateTime.ParseExact("2021-11-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            });

            await dbContext.Concerts.AddAsync(new Concert
            {
                Name = "Varna Rock 2019",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376588/concerts_photos/Varna_Rock_2019_tougvh.jpg",
                Date = DateTime.ParseExact("2019-08-14 18:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            });
        }
    }
}
