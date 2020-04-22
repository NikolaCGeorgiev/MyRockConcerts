namespace MyRockConcerts.Web.ViewModels.InputModels.Members
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class MemberEditInputModel : IMapFrom<Member>
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public IFormFile Photo { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Group")]
        public int GroupId { get; set; }

        public IEnumerable<GroupDropDownViewModel> Groups { get; set; }
    }
}
