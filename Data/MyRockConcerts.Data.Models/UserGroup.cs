namespace MyRockConcerts.Data.Models
{
    public class UserGroup
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
