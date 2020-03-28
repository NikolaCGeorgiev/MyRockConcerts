namespace MyRockConcerts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Venue : BaseDeletableModel<int>
    {
        public Venue()
        {
            this.Concerts = new HashSet<Concert>();
        }

        [Required]
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

        public int Capacity { get; set; }

        public virtual ICollection<Concert> Concerts { get; set; }
    }
}
