namespace MyRockConcerts.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Common;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.Infrastructure;
    using MyRockConcerts.Web.ViewModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IConcertsService concertsService;

        public HomeController(IConcertsService concertsService)
        {
            this.concertsService = concertsService;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(nameof(this.IndexLoggedIn));
            }

            var viewModel = new GuestIndexViewModel
            {
                Concerts = this.concertsService.GetAllUpcoming<GuestConcertViewModel>(2).ToList(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpGet]
        [Route("/Home/Index")]
        public async Task<IActionResult> IndexLoggedIn(int? pageNumber)
        {
            var viewModel = this.concertsService.GetAllUpcoming<LoggedInConcertViewModel>();

            return this.View(await PaginatedList<LoggedInConcertViewModel>
                .CreateAsync(viewModel, pageNumber ?? GlobalConstants.DefaultPageNumber, GlobalConstants.PageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == StatusCodes.NotFound)
            {
                return this.Redirect($"/Error/{StatusCodes.NotFound}");
            }

            return this.Redirect($"/Error/{StatusCodes.InternalServerError}");
        }
    }
}
