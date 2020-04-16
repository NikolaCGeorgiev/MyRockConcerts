namespace MyRockConcerts.Web.ViewModels.InputModels.Albums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class AlbumCreateInputModel : IMapTo<Album>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Group")]
        public int GroupId { get; set; }

        public IEnumerable<GroupDropDownViewModel> Groups { get; set; }
    }
}
