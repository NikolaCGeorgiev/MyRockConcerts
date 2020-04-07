namespace MyRockConcerts.Web.ViewModels.Concerts
{
    using System;
    using System.Collections.Generic;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class ConcertViewModel : IMapFrom<Concert>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public DateTime Date { get; set; }

        public string TicketUrl { get; set; }

        public int VenueId { get; set; }

        public string VenueCountry { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public IEnumerable<GroupInfoViewModel> Groups { get; set; }
    }
}
