namespace MyRockConcerts.Web.ViewModels.Groups
{
    using System.Collections.Generic;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Albums;
    using MyRockConcerts.Web.ViewModels.Genres;
    using MyRockConcerts.Web.ViewModels.Members;

    public class GroupDetailsViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public IEnumerable<AlbumDetailsViewModel> Albums { get; set; }

        public IEnumerable<MemberDetailsViewModel> Members { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
