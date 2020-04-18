namespace MyRockConcerts.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewComponents.Models;

    public class GenresDropdownViewComponent : ViewComponent
    {
        private readonly IGenresService genresService;

        public GenresDropdownViewComponent(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await this.genresService.AllAsync<GenresDropdownViewModel>();

            return this.View(genres);
        }
    }
}
