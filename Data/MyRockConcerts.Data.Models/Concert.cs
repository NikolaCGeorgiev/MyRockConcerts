namespace MyRockConcerts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Concert : BaseDeletableModel<int>
    {
        public Concert()
        {
            this.ConcertGroups = new HashSet<ConcertGroup>();
            this.UserConcerts = new HashSet<UserConcert>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public DateTime Date { get; set; }

        public int OrganiserId { get; set; }

        public virtual Organiser Organiser { get; set; }

        public int VenueId { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual ICollection<ConcertGroup> ConcertGroups { get; set; }

        public virtual ICollection<UserConcert> UserConcerts { get; set; }
    }
}
