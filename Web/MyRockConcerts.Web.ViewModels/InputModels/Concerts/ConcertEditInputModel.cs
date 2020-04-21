using Microsoft.AspNetCore.Http;
using MyRockConcerts.Data.Models;
using MyRockConcerts.Services.Mapping;
using MyRockConcerts.Web.ViewModels.Venues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRockConcerts.Web.ViewModels.InputModels.Concerts
{
    public class ConcertEditInputModel : IMapFrom<Concert>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public IFormFile Photo { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string TicketUrl { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        public Venue Venue { get; set; }

        public IEnumerable<VenuesDropDownViewModel> Venues { get; set; }
    }
}
