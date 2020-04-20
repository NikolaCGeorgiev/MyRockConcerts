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
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377132/members_photos/Joakim_Broden_ebonyk.jpg",
                Description = "Joakim Brodén is a Swedish-Czech singer and songwriter who is the lead vocalist, keyboardist, and occasional third guitarist of the Swedish power metal band Sabaton.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Tommy Johansson",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377194/members_photos/Tommy_Johansson_k1zchv.jpg",
                Description = "Tommy Johansson, is a Swedish vocalist and guitarist known for his work as a guitarist for Swedish metal band Sabaton and as lead vocalist and guitarist of the band Majestica, a power metal band from Boden, Sweden.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Hannes Van Dahl",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377252/members_photos/Hannes_Van_Dahl_qdrcau.jpg",
                Description = "Dahl started his career in a band called Evergrey. He departed that band in 2013 and joined Sabaton. He is currently in a relationship with Nightwish singer Floor Jansen.",
                GroupId = 4,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Nils Patrik Johansson",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377312/members_photos/Nils_Patrik_Johansson_k3vluf.jpg",
                Description = "Nils Patrik Johansson is a Swedish heavy metal singer who has been the lead singer of Astral Doors since 2002 and was the lead vocalist of Civil War from 2012 to 2016.",
                GroupId = 3,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "James Hetfield",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377372/members_photos/James_Hetfield_tzaeyy.jpg",
                Description = "James Alan Hetfield (born August 3, 1963) is an American musician and songwriter best known for being the co-founder, lead vocalist/rhythm guitarist and main songwriter for the American heavy metal band Metallica.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Lars Ulrich",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377418/members_photos/Lars_Ulrich_rriamk.jpg",
                Description = "Lars Ulrich R is a Danish musician, songwriter, record producer and podcaster. He is best known as the drummer and co-founder of the American heavy metal band Metallica.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Kirk Hammett",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377469/members_photos/Kirk_Hammett_hxukfq.jpg",
                Description = "Kirk Lee Hammett is an American musician who has been lead guitarist and a contributing songwriter for the heavy metal band Metallica since 1983.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Robert Trujillo",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377519/members_photos/Robert_Trujillo_eh4fuc.jpg",
                Description = "Robert Trujillo is an American musician and songwriter. He has been the bassist of the American heavy metal band Metallica since 2003.",
                GroupId = 2,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Tomas Olsson",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377569/members_photos/Tomas_Olsson_yfervl.jpg",
                Description = "Guitarist for Swedish Power Metal band Bloodbound.",
                GroupId = 1,
            });

            await dbContext.Members.AddAsync(new Member
            {
                FullName = "Fredrik Bergh",
                ImgUrl = "https://res.cloudinary.com/nikolacgeorgiev/image/upload/v1587377625/members_photos/Fredrik_Bergh_srmmea.jpg",
                Description = "Fredrik Bergh is a keyboard player and songwriter. He is also a founding member and keyboard player in the Swedish heavy metal band Bloodbound.",
                GroupId = 1,
            });
        }
    }
}
