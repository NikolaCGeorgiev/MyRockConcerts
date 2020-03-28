namespace MyRockConcerts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Organiser : BaseDeletableModel<int>
    {
        public Organiser()
        {
            this.Concerts = new HashSet<Concert>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual ICollection<Concert> Concerts { get; set; }
    }
}
