namespace MyRockConcerts.Web.ViewModels.InputModels.Albums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class AlbumCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Group")]
        public int GroupId { get; set; }

        public IEnumerable<GroupDropDownViewModel> Groups { get; set; }
    }
}
