namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            await dbContext.Genres.AddAsync(new Genre { Name = "Heavy metal" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Power metal" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Thrash metal" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Symphonic metal" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Gothic metal" });
        }
    }
}
