namespace MyRockConcerts.Web.ViewModels.InputModels.Venues
{
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class VenueCreateInputModel : IMapTo<Venue>
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }
    }
}
