namespace MyRockConcerts.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public class MembersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Members.Any())
            {
                return;
            }

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Joakim Broden",
                ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/6/65/Sabaton_%E2%80%93_Elbriot_2016_08.jpg",
                Description = "Joakim Brodén is a Swedish-Czech singer and songwriter who is the lead vocalist, keyboardist, and occasional third guitarist of the Swedish power metal band Sabaton.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Tommy Johansson",
                ImgUrl = "https://live.staticflickr.com/4274/34559545093_a4d0a2541d_b.jpg",
                Description = "Tommy Johansson, is a Swedish vocalist and guitarist known for his work as a guitarist for Swedish metal band Sabaton and as lead vocalist and guitarist of the band Majestica, a power metal band from Boden, Sweden.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Hannes Van Dahl",
                ImgUrl = "https://i.pinimg.com/originals/39/a6/ee/39a6ee09a973d7a4b4e92849baec4a16.jpg",
                Description = "Dahl started his career in a band called Evergrey. He departed that band in 2013 and joined Sabaton. He is currently in a relationship with Nightwish singer Floor Jansen.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Nils Patrik Johansson",
                ImgUrl = "https://www.hardrock.hu/img/taz/hirek/1804/nilsp.jpg",
                Description = "Nils Patrik Johansson is a Swedish heavy metal singer who has been the lead singer of Astral Doors since 2002 and was the lead vocalist of Civil War from 2012 to 2016.",
                GroupId = 3,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "James Hetfield",
                ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/James_Hetfield_2017.jpg/800px-James_Hetfield_2017.jpg",
                Description = "James Alan Hetfield (born August 3, 1963) is an American musician and songwriter best known for being the co-founder, lead vocalist/rhythm guitarist and main songwriter for the American heavy metal band Metallica.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Lars Ulrich",
                ImgUrl = "https://metalheadzone.com/wp-content/uploads/2019/05/lars-ulrich-2019.jpg",
                Description = "Lars Ulrich R is a Danish musician, songwriter, record producer and podcaster. He is best known as the drummer and co-founder of the American heavy metal band Metallica.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Kirk Hammett",
                ImgUrl = "https://c12.incisozluk.com.tr/res/incisozluk//11505/9/3342289_o0446.jpg",
                Description = "Kirk Lee Hammett is an American musician who has been lead guitarist and a contributing songwriter for the heavy metal band Metallica since 1983.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Robert Trujillo",
                ImgUrl = "https://cdn.mos.cms.futurecdn.net/VvboKZCmjutQadCjycdJ2k.jpg",
                Description = "Robert Trujillo is an American musician and songwriter. He has been the bassist of the American heavy metal band Metallica since 2003.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Tomas Olsson",
                ImgUrl = "https://img.discogs.com/NKjfbm-iaR3u_uOP-9BxP8URyX4=/600x737/smart/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/A-2715828-1448434711-2870.jpeg.jpg",
                Description = "Guitarist for Swedish Power Metal band Bloodbound.",
                GroupId = 1,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Fredrik Bergh",
                ImgUrl = "https://myglobalmind.com/wp-content/uploads/2016/03/Bloodbound_2.jpg",
                Description = "Fredrik Bergh is a keyboard player and songwriter. He is also a founding member and keyboard player in the Swedish heavy metal band Bloodbound.",
                GroupId = 1,
            });
        }
    }
}
