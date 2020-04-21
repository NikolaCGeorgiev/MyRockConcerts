namespace MyRockConcerts.Web.ViewModels.InputModels.Groups
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Services.Mapping;

    public class GroupCreateInputModel : IMapTo<GroupServiceModel>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public IFormFile ImgUrl { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
