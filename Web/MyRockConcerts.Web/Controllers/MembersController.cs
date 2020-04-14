namespace MyRockConcerts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Members;

    public class MembersController : BaseController
    {
        private readonly IMembersService membersService;

        public MembersController(IMembersService membersService)
        {
            this.membersService = membersService;
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var member = await this.membersService.GetByIdAsync<MemberDetailsViewModel>(id);

            if (member == null)
            {
                return this.NotFound();
            }

            return this.View(member);
        }
    }
}
