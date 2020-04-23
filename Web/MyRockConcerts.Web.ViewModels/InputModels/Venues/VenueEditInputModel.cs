namespace MyRockConcerts.Web.ViewModels.InputModels.Venues
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class VenueEditInputModel : IMapFrom<Venue>
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public IFormFile Photo { get; set; }

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
