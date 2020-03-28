namespace MyRockConcerts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.GroupGenres = new HashSet<GroupGenre>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<GroupGenre> GroupGenres { get; set; }
    }
}
