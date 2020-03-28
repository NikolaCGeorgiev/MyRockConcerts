namespace MyRockConcerts.Data.Models
{
    public class ConcertGroup
    {
        public int ConcertId { get; set; }

        public virtual Concert Concert { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
