namespace MyRockConcerts.Web.ViewModels.InputModels.Genres
{
    using System.ComponentModel.DataAnnotations;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GenreCreateInputModel : IMapTo<Genre>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
