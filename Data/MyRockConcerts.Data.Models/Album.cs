namespace MyRockConcerts.Data.Models
{
    using System;

    using MyRockConcerts.Data.Common.Models;

    public class Album : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string CoverUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
