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
                Name = "Hills Of Rock 2020",
                ImgUrl = "https://hillsofrock.com/public/resources/images/day1/5.jpg",
                Date = DateTime.ParseExact("2020-07-24", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/hills-of-rock-2020-plovdiv-rowing-canal-1181470/performance.html",
                VenueId = 2,
            });

            await dbContext.Concerts.AddAsync(new Concert
            {
                Name = "Varna Rock 2020",
                ImgUrl = "https://i2.wp.com/rockthenight.eu/wp/wp-content/uploads/2019/08/2020.08.14-varna-rock.jpg?fit=960%2C360&ssl=1",
                Date = DateTime.ParseExact("2020-08-14", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                TicketUrl = "https://www.eventim.bg/bg/bileti/varna-rock-2020-barna-plaz-asparuhovo-1148430/performance.html",
                VenueId = 3,
            });
        }
    }
}
