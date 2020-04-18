namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.Genres;
    using MyRockConcerts.Web.ViewModels.InputModels.Groups;

    public class GroupsController : AdministrationController
    {
        private const string CreateSuccessMessage = "You successfully added a group!";
        private const string AddGenreSuccessMessage = "You successfully added a genre!";
        private const string RemoveGenreSuccessMessage = "You successfully removed a genre!";

        private readonly IGroupsService groupsService;
        private readonly IGenresService genresService;

        public GroupsController(
            IGroupsService groupsService,
            IGenresService genresService)
        {
            this.groupsService = groupsService;
            this.genresService = genresService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GroupCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var id = await this.groupsService.CreateAsync(input.Name, input.ImgUrl, input.Description);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Groups/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.View(nameof(this.Create));
            }
        }

        [Authorize]
        public async Task<IActionResult> AddGenre(int id)
        {
            var genres = await this.genresService.AllAsync<GenreViewModel>();

            var viewModel = new AddGenresViewModel
            {
                Id = id,
                Genres = genres,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddGenre(AddGenresViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var id = await this.genresService.AddGenreAsync(input.Id, input.GenreId);

                this.TempData["Success"] = AddGenreSuccessMessage;
                return this.Redirect("/Groups/Details/" + id);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.RedirectToAction(nameof(this.AddGenre));
            }
        }

        [Authorize]
        public async Task<IActionResult> RemoveGenre(int id, int genreId)
        {
            var groupId = await this.genresService.RemoveGenreAsync(id, genreId);

            this.TempData["Success"] = RemoveGenreSuccessMessage;

            return this.Redirect("/Groups/Details/" + groupId);
        }
    }
}
