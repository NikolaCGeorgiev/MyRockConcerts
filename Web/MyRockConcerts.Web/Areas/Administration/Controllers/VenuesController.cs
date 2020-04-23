namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.InputModels.Venues;
    using MyRockConcerts.Web.ViewModels.Venues;

    public class VenuesController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully added a venue!";
        private const string EditSuccessMessage = "You have successfully edited a venue!";

        private readonly IVenuesService venuesService;
        private readonly ICloudinaryService cloudinaryService;

        public VenuesController(
            IVenuesService venuesService,
            ICloudinaryService cloudinaryService)
        {
            this.venuesService = venuesService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var venues = await this.venuesService.GetAllAsync<VanueDetailsViewModel>();

            var viewModel = new VenuesListViewModel
            {
                Venues = venues,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(VenueCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var id = await this.venuesService
                    .CreateAsync(input.Name, input.ImgUrl, input.Country, input.City, input.Address, input.Capacity);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Venues/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.View(input);
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var venue = await this.venuesService.GetByIdAsync<VenueEditInputModel>(id);

            if (venue == null)
            {
                return this.NotFound();
            }

            return this.View(venue);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, VenueEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.venuesService.EditAsync(id, input);

                this.TempData["Success"] = EditSuccessMessage;
                return this.Redirect("/Venues/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.View(input);
            }
        }
    }
}
