namespace MyRockConcerts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Genres;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var genres = await this.genresService.AllAsync<GenreViewModel>();

            var viewModel = new GenresListViewModel
            {
                Genres = genres,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> GetGroups(int id)
        {
            var genres = await this.genresService.AllAsync<GenreViewModel>();

            var viewModel = new GenresListViewModel
            {
                Genres = genres,
            };

            return this.View(viewModel);
        }
    }
}
