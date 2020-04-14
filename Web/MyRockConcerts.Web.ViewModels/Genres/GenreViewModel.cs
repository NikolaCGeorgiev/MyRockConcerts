namespace MyRockConcerts.Web.ViewModels.Genres
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
