namespace MyRockConcerts.Web.ViewModels.InputModels.Groups
{
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupCreateInputModel : IMapTo<Group>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
