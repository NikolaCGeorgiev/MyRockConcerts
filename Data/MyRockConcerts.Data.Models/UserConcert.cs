namespace MyRockConcerts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserConcert
    {
        public int ConcertId { get; set; }

        public virtual Concert Concert { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
