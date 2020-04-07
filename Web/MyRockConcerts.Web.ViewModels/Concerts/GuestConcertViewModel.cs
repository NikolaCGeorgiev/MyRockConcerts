namespace MyRockConcerts.Web.ViewModels.Concerts
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GuestConcertViewModel : IMapFrom<Concert>
    {
        public string ImgUrl { get; set; }
    }
}
