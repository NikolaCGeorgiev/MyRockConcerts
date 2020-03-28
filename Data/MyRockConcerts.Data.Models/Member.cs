namespace MyRockConcerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Common.Models;

    public class Member : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
