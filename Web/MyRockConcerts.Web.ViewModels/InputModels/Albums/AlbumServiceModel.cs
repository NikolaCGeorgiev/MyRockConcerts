namespace MyRockConcerts.Web.ViewModels.InputModels.Albums
{
    using System;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class AlbumServiceModel : IMapTo<Album>, IMapFrom<Album>
    {
        public string Name { get; set; }

        public string CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GroupId { get; set; }
    }
}
