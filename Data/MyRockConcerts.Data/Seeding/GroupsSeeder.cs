namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class GroupsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Groups.Any())
            {
                return;
            }

            await dbContext.Groups.AddAsync(new Group
            {
                Name = "Sabaton",
                ImgUrl = "https://www.spark-rockmagazine.cz/files/3543f26d360086d8d8db81fff24b8ea3.jpg",
                Description = "Sabaton is a Swedish heavy metal band from Falun. The band's main lyrical themes are based on war, historical battles, and acts of heroism—the name is a reference to a sabaton, knight's foot armor.",
            });

            await dbContext.Groups.AddAsync(new Group
            {
                Name = "Civil War",
                ImgUrl = "https://www.metal-observer.com/wordpress/wp-content/uploads/2015/05/Civil-War-band.jpg",
                Description = "Civil War is a power metal band from Falun, Sweden formed in 2012 by several former members of Sabaton. The band adopted the same lyrical themes of war and historical battles that were characteristic of Sabaton.",
            });

            await dbContext.Groups.AddAsync(new Group
            {
                Name = "Metallica",
                ImgUrl = "https://media.socastsrm.com/wordpress/wp-content/blogs.dir/684/files/2018/02/metallicaheavymontreal2017.jpg",
                Description = "Metallica is an American heavy metal band. The band was formed in 1981 in Los Angeles by vocalist/guitarist James Hetfield and drummer Lars Ulrich, and has been based in San Francisco for most of its career.",
            });

            await dbContext.Groups.AddAsync(new Group
            {
                Name = "Bloodbound",
                ImgUrl = "https://img.discogs.com/l7FIP8zojtvQ0rEb8EYLwE0yWhI=/600x405/smart/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/A-1751229-1448435331-2423.jpeg.jpg",
                Description = "Bloodbound is a Swedish power metal band formed in 2004. They released their first studio album, Nosferatu, in 2005 and their second, Book of the Dead, in May 2007.",
            });
        }
    }
}
