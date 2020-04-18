namespace MyRockConcerts.Web.ViewModels.Genres
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class AddGenresViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
