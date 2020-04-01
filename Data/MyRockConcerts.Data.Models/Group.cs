namespace MyRockConcerts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Group : BaseDeletableModel<int>
    {
        public Group()
        {
            this.Members = new HashSet<Member>();
            this.ConcertGroups = new HashSet<ConcertGroup>();
            this.GroupGenres = new HashSet<GroupGenre>();
            this.Albums = new HashSet<Album>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<ConcertGroup> ConcertGroups { get; set; }

        public virtual ICollection<GroupGenre> GroupGenres { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
