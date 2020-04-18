namespace MyRockConcerts.Web.ViewModels.Genres
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddGenresViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
    }
}
