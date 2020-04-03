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
                ImgUrl = "https://www.rockarena.co.uk/wp-content/uploads/2018/08/mo8Q4GVMi2cd9q30CtRf-32905.jpg",
                Country = "Bulgaria",
                City = "Varna",
                Address = "Sq. Slaveykov 1",
                Capacity = 30000,
            });

            await dbContext.Venues.AddAsync(new Venue
            {
                Name = "Rowing Canal",
                ImgUrl = "https://nstatic.nova.bg/public/pics/nova/article/980x551_1532605568.jpg",
                Country = "Bulgaria",
                City = "Plovdiv",
                Address = "Yasna Polyana",
                Capacity = 70000,
            });

            await dbContext.Venues.AddAsync(new Venue
            {
                Name = "Arena Armeec",
                ImgUrl = "https://i.pik.bg/news/695/660_7effc601dd3d7ed9f86b71ad88124d31.jpg",
                Country = "Bulgaria",
                City = "Sofia",
                Address = "Asen Yordanov 1",
                Capacity = 45000,
            });
        }
    }
}
