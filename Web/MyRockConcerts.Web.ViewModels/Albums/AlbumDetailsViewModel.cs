namespace MyRockConcerts.Web.ViewModels.Albums
{
    using System;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class AlbumDetailsViewModel : IMapFrom<Album>
    {
        public string Name { get; set; }

        public string CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
