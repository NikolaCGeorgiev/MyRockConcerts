namespace MyRockConcerts.Web.ViewModels.Genres
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GenresViewModel : IMapFrom<Genre>
    {
        public string Name { get; set; }
    }
}
