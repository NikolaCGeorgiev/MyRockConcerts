namespace MyRockConcerts.Web.ViewModels.InputModels.Groups
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupEditInputModel : IMapFrom<Group>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public IFormFile Photo { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
