namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.InputModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Venues;
    using System;
    using System.Threading.Tasks;

    public class ConcertsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You successfully added a concert!";

        private readonly IVenuesService venuesService;
        private readonly IConcertsService concertsService;

        public ConcertsController(
            IVenuesService venuesService,
            IConcertsService concertsService)
        {
            this.venuesService = venuesService;
            this.concertsService = concertsService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var venues = await this.venuesService.GetAllAsync<VenuesDropDownViewModel>();

            var viewModel = new GoncertCreateInputModel
            {
                Venues = venues,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GoncertCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var id = await this.concertsService.CreateAsync(input.Name, input.ImgUrl, input.Date, input.TicketUrl, input.VenueId);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Concerts/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.RedirectToAction(nameof(this.Create));
            }
        }
    }
}
