namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Albums;
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.InputModels.Albums;

    public class AlbumsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully added an album!";
        private const string EditSuccessMessage = "You have successfully edited an album!";

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
        public async Task<IActionResult> All()
        {
            var albums = await this.albumsService.GetAllAsync<AlbumInfoViewModel>();

            var viewModel = new AlbumsListViewModel
            {
                Albums = albums,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id = null)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            var viewModel = new AlbumCreateInputModel
            {
                Groups = groups,
            };

            this.TempData["groupId"] = id;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AlbumCreateInputModel input)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            if (!this.ModelState.IsValid)
            {
                input.Groups = groups;

                return this.View(input);
            }

            try
            {
                var id = await this.albumsService
                    .CreateAsync(input.Name, input.CoverUrl, input.ReleaseDate, input.GroupId);

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
            var album = await this.albumsService.GetByIdAsync<AlbumEditInputModel>(id);

            if (album == null)
            {
                return this.NotFound();
            }

            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            album.Groups = groups;

            return this.View(album);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, AlbumEditInputModel input)
        {
            var groups = await this.groupsService.GetAll<GroupDropDownViewModel>().ToListAsync();

            if (!this.ModelState.IsValid)
            {
                input.Groups = groups;

                return this.View(input);
            }

            try
            {
                await this.albumsService.EditAsync(id, input);

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
