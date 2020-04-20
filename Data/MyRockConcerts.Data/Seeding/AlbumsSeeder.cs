namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class AlbumsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Albums.Any())
            {
                return;
            }

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "Primo Victoria",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375808/albums_photos/Primo_Victoria_xhi3ny.jpg",
                ReleaseDate = DateTime.ParseExact("2005-03-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "Carolus Rex",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375890/albums_photos/Carolus_Rex_ftub8r.jpg",
                ReleaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "The Killer Angels",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587375953/albums_photos/The_Killer_Angels_m8jxti.jpg",
                ReleaseDate = DateTime.ParseExact("2013-06-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 3,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "...And Justice for All",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376033/albums_photos/...And_Justice_for_All_ijojcr.jpg",
                ReleaseDate = DateTime.ParseExact("1988-08-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 2,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "Rise Of The Dragon Empire",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376104/albums_photos/Rise_Of_The_Dragon_Empire_iwmc92.jpg",
                ReleaseDate = DateTime.ParseExact("2019-03-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 1,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "War of Dragons",
                CoverUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587376177/albums_photos/War_of_Dragons_ti2avv.jpg",
                ReleaseDate = DateTime.ParseExact("2017-02-24", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 1,
            });
        }
    }
}
