namespace MyRockConcerts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Common;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.Infrastructure;
    using MyRockConcerts.Web.ViewModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class ConcertsController : BaseController
    {
        private readonly IConcertsService concertsService;
        private readonly IGroupsService groupsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ConcertsController(
            IConcertsService concertsService,
            IGroupsService groupsService,
            UserManager<ApplicationUser> userManager)
        {
            this.concertsService = concertsService;
            this.groupsService = groupsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var groups = await this.groupsService.GetGroupsByConcertIdAsync<GroupInfoViewModel>(id);
            var concert = await this.concertsService.GetByIdAsync<ConcertViewModel>(id);

            if (concert == null)
            {
                return this.NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            concert.IsInMyConcerts = await this.concertsService.IsInMyConcertsAsync(id, userId);
            concert.Groups = groups;

            return this.View(concert);
        }

        [Authorize]
        public async Task<IActionResult> AddToMyConcerts(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.concertsService.AddToMyConcertsAsync(id, userId);

            return this.Redirect("/Concerts/MyConcerts");
        }

        [Authorize]
        public async Task<IActionResult> MyConcerts(int? pageNumber)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.concertsService.GetMyConcerts<LoggedInConcertViewModel>(userId);

            return this.View(await PaginatedList<LoggedInConcertViewModel>
                .CreateAsync(viewModel, pageNumber ?? GlobalConstants.DefaultPageNumber, GlobalConstants.PageSize));
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromMyConcerts(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.concertsService.RemoveFromMyConcertsAsync(id, userId);

            return this.Redirect("/Concerts/MyConcerts");
        }
    }
}
