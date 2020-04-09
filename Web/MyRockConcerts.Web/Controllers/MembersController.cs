using Microsoft.AspNetCore.Mvc;
using MyRockConcerts.Services.Data;
using MyRockConcerts.Web.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRockConcerts.Web.Controllers
{
    public class MembersController : BaseController
    {
        private readonly IMembersService membersService;

        public MembersController(IMembersService membersService)
        {
            this.membersService = membersService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var member = await this.membersService.GetMemberIdAsync<MemberDetailsViewModel>(id);

            if (member == null)
            {
                return this.NotFound();
            }

            return this.View(member);
        }
    }
}
