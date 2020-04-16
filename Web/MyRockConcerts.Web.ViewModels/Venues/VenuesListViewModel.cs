namespace MyRockConcerts.Web.ViewModels.Venues
{
    using System.Collections.Generic;

    public class VenuesListViewModel
    {
        public IEnumerable<VanueDetailsViewModel> Venues { get; set; }
    }
}
