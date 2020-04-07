namespace MyRockConcerts.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using MyRockConcerts.Web.ViewModels.Concerts;

    public class GuestIndexViewModel
    {
        public IEnumerable<GuestConcertViewModel> Concerts { get; set; }
    }
}
