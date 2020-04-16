using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRockConcerts.Services.Data;
using MyRockConcerts.Web.ViewModels.Groups;
using MyRockConcerts.Web.ViewModels.InputModels.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    public class AlbumsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You successfully created album!";

        private readonly IGroupsService groupsService;
        private readonly IAlbumsService albumsService;

        public AlbumsController(
            IGroupsService groupsService,
            IAlbumsService albumsService)
        {
            this.groupsService = groupsService;
            this.albumsService = albumsService;
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id = null)
        {
            if (id == null)
            {
                var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

                var viewModel = new AlbumCreateInputModel
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
        public async Task<IActionResult> Create(AlbumCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            try
            {
                var id = await this.albumsService.CreateAsync(input.Name, input.CoverUrl, input.ReleaseDate, input.GroupId);

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
