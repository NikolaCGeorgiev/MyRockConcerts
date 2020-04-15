namespace MyRockConcerts.Web.ViewModels.Venues
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class VanueDetailsViewModel : IMapFrom<Venue>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int Capacity { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
