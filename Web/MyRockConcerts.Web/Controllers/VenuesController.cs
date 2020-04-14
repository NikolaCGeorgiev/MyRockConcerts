namespace MyRockConcerts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Venues;

    public class VenuesController : BaseController
    {
        private readonly IVenuesService venuesService;

        public VenuesController(IVenuesService venuesService)
        {
            this.venuesService = venuesService;
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var venue = await this.venuesService.GetByIdAsync<VanueDetailsViewModel>(id);

            return this.View(venue);
        }
    }
}
