namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.InputModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Venues;

    public class ConcertsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully added a concert!";
        private const string AddGroupSuccessMessage = "You have successfully added a group!";
        private const string RemoveGroupSuccessMessage = "You have successfully removed the group!";

        private readonly IGroupsService groupsService;
        private readonly IVenuesService venuesService;
        private readonly IConcertsService concertsService;

        public ConcertsController(
            IGroupsService groupsService,
            IVenuesService venuesService,
            IConcertsService concertsService)
        {
            this.groupsService = groupsService;
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

        [Authorize]
        public async Task<IActionResult> AddGroup(int id)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            var viewModel = new AddGroupViewModel
            {
                Id = id,
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddGroup(AddGroupViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var id = await this.groupsService.AddGroupAsync(input.Id, input.GroupId);

                this.TempData["Success"] = AddGroupSuccessMessage;
                return this.Redirect("/Concerts/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.RedirectToAction(nameof(this.AddGroup));
            }
        }

        [Authorize]
        public async Task<IActionResult> RemoveGroup(int id, int groupId)
        {
            var concertId = await this.groupsService.RemoveGroupAsync(id, groupId);

            this.TempData["Success"] = RemoveGroupSuccessMessage;

            return this.Redirect("/Concerts/Details/" + concertId);
        }
    }
}
