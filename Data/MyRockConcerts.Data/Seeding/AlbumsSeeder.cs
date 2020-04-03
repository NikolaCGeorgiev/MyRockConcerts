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
                CoverUrl = "https://upload.wikimedia.org/wikipedia/en/8/8f/Primo_Victoria_album_cover.jpg",
                ReleaseDate = DateTime.ParseExact("2005-03-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "Carolus Rex",
                CoverUrl = "https://www.nuclearblast.de/static/articles/202/202580.jpg/400x400.jpg",
                ReleaseDate = DateTime.ParseExact("2012-05-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 4,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "The Killer Angels",
                CoverUrl = "https://img.discogs.com/bQcUWesDp6pONmpQvNWKWTfcqgw=/fit-in/575x519/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-4635118-1373745158-2046.jpeg.jpg",
                ReleaseDate = DateTime.ParseExact("2013-06-04", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 3,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "...And Justice for All",
                CoverUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/Metallica_-_...And_Justice_for_All_cover.jpg",
                ReleaseDate = DateTime.ParseExact("1988-08-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 2,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "Rise Of The Dragon Empire",
                CoverUrl = "https://img.discogs.com/J856TtBPJjzA9byds1zh8GCLcXE=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-13379513-1553098379-2629.jpeg.jpg",
                ReleaseDate = DateTime.ParseExact("2019-03-22", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 1,
            });

            await dbContext.Albums.AddAsync(new Album
            {
                Name = "War of Dragons",
                CoverUrl = "https://img.discogs.com/U6CgtTNR-x_cpkVMj3UvAMcq_Wo=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-9839673-1487167503-2676.jpeg.jpg",
                ReleaseDate = DateTime.ParseExact("2017-02-24", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                GroupId = 1,
            });
        }
    }
}
