namespace MyRockConcerts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Concerts;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class ConcertsController : BaseController
    {
        private readonly IConcertsService concertsService;
        private readonly IGroupsService groupsService;

        public ConcertsController(IConcertsService concertsService, IGroupsService groupsService)
        {
            this.concertsService = concertsService;
            this.groupsService = groupsService;
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

            concert.Groups = groups;

            return this.View(concert);
        }
    }
}
