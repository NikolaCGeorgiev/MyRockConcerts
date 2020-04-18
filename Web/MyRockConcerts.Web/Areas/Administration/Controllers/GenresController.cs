namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.ViewModels.InputModels.Genres;

    public class GenresController : AdministrationController
    {
        private const string CreateSuccessMessage = "You have successfully created a genre!";

        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GenreCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.genresService.CreateAsync(input.Name);

                this.TempData["Success"] = CreateSuccessMessage;
                return this.Redirect("/Genres/All");
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;

                return this.View(nameof(this.Create));
            }
        }
    }
}
