namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Common;
    using MyRockConcerts.Services;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Albums;
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.InputModels.Albums;

    public class AlbumsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully added an album!";

        private readonly IGroupsService groupsService;
        private readonly IAlbumsService albumsService;
        private readonly ICloudinaryService cloudinaryService;

        public AlbumsController(
            IGroupsService groupsService,
            IAlbumsService albumsService,
            ICloudinaryService cloudinaryService)
        {
            this.groupsService = groupsService;
            this.albumsService = albumsService;
            this.cloudinaryService = cloudinaryService;
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
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            var coverUrl = await this.cloudinaryService.UploadPhotoAsync(
               input.CoverUrl,
               input.Name,
               GlobalConstants.CloudFolderForAlbumsPhotos);

            var serviceModel = new AlbumServiceModel
            {
                Name = input.Name,
                CoverUrl = coverUrl,
                ReleaseDate = input.ReleaseDate,
                GroupId = input.GroupId,
            };

            try
            {
                var id = await this.albumsService.CreateAsync(serviceModel);

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
