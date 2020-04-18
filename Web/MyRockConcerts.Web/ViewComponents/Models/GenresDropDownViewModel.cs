namespace MyRockConcerts.Web.ViewComponents.Models
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GenresDropdownViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
