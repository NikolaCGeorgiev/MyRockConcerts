using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRockConcerts.Services.Data;
using MyRockConcerts.Web.ViewModels.InputModels.Venues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    public class VenuesController : AdministrationController
    {
        private const string CreateSuccessMessage = "You successfully created venue!";

        private readonly IVenuesService venuesService;

        public VenuesController(IVenuesService venuesService)
        {
            this.venuesService = venuesService;
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
                await this.venuesService.CreateAsync(input.Name, input.ImgUrl, input.Country, input.City, input.Address, input.Capacity);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Home/Index");
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.View(nameof(this.Create));
            }
        }
    }
}
