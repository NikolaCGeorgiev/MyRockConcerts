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
    using MyRockConcerts.Web.ViewModels.Members;

    public class MembersController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully added a member!";
        private const string EditSuccessMessage = "You have successfully edited a member!";

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
        public async Task<IActionResult> All()
        {
            var members = await this.membersService.GetAllAsync<MemberInfoViewModel>();

            var viewModel = new MemebersListViewModel
            {
                Members = members,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id = null)
        {
            if (id != null)
            {
                this.TempData["groupId"] = id;

                return this.View();
            }

            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            var viewModel = new MemberCreateInputModel
            {
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MemberCreateInputModel input)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            if (!this.ModelState.IsValid)
            {
                input.Groups = groups;

                return this.View(input);
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

                input.Groups = groups;

                return this.View(input);
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await this.membersService.GetByIdAsync<MemberEditInputModel>(id);

            if (member == null)
            {
                return this.NotFound();
            }

            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            member.Groups = groups;

            return this.View(member);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, MemberEditInputModel input)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            if (!this.ModelState.IsValid)
            {
                input.Groups = groups;

                return this.View(input);
            }

            try
            {
                await this.membersService.EditAsync(id, input);

                this.TempData["Success"] = EditSuccessMessage;
                return this.Redirect("/Groups/Details/" + input.GroupId);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                input.Groups = groups;

                return this.View(input);
            }
        }
    }
}
