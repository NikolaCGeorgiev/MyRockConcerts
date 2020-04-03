namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class ConcertGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ConcertGroups.Any())
            {
                return;
            }

            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 2, GroupId = 2 });
            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 2, GroupId = 3 });
            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 2, GroupId = 4 });
            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 1, GroupId = 1 });
            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 1, GroupId = 3 });
            await dbContext.ConcertGroups.AddAsync(new ConcertGroup { ConcertId = 1, GroupId = 4 });
        }
    }
}
