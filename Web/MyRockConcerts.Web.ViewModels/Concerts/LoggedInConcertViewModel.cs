namespace MyRockConcerts.Web.ViewModels.Concerts
{
    using System;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class LoggedInConcertViewModel : IMapFrom<Concert>
    {
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public DateTime Date { get; set; }

        public string VenueCountry { get; set; }

        public string VenueCity { get; set; }
    }
}
