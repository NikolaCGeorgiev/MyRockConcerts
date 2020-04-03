namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class GroupGenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GroupGenres.Any())
            {
                return;
            }

            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 1, GenreId = 1 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 1, GenreId = 2 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 2, GenreId = 1 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 2, GenreId = 3 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 3, GenreId = 2 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 4, GenreId = 1 });
            await dbContext.GroupGenres.AddAsync(new GroupGenre { GroupId = 4, GenreId = 2 });
        }
    }
}
