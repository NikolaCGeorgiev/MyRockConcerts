﻿namespace MyRockConcerts.Web.ViewModels.InputModels.Concerts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Venues;

    public class GoncertCreateInputModel : IMapTo<Concert>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string TicketUrl { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        public IEnumerable<VenuesDropDownViewModel> Venues { get; set; }
    }
}
