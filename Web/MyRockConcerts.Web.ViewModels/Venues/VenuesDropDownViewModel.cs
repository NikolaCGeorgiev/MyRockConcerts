namespace MyRockConcerts.Web.ViewModels.Venues
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class VenuesDropDownViewModel : IMapFrom<Venue>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
