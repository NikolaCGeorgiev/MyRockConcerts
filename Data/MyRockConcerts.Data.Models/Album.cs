namespace MyRockConcerts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Album : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
