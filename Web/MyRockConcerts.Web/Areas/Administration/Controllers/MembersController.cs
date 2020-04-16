namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.InputModels.Members;

    public class MembersController : AdministrationController
    {
        private const string CreateSuccessMessage = "You successfully added a member!";

        private readonly IGroupsService groupsService;
        private readonly IMembersService membersService;

        public MembersController(
            IGroupsService groupsService,
            IMembersService membersService)
        {
            this.groupsService = groupsService;
            this.membersService = membersService;
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id = null)
        {
            if (id == null)
            {
                var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

                var viewModel = new MemberCreateInputModel
                {
                    Groups = groups,
                };

                return this.View(viewModel);
            }

            this.TempData["groupId"] = id;

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MemberCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            try
            {
                var id = await this.membersService.CreateAsync(input.FullName, input.ImgUrl, input.Description, input.GroupId);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Groups/Details/" + input.GroupId);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.RedirectToAction(nameof(this.Create));
            }
        }
    }
}
