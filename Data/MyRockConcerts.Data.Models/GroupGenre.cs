namespace MyRockConcerts.Data.Models
{
    public class GroupGenre
    {
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
