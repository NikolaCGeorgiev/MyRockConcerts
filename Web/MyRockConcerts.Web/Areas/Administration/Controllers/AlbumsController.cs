﻿namespace MyRockConcerts.Web.Areas.Administration.Controllers
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
        private const string CreateSuccessMessage = "You successfully added an album!";

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

            return this.Redirect("/Administration/Albums/Create");
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