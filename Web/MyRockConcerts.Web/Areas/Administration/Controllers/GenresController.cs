namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GenresController : AdministrationController
    {
        public IActionResult Create()
        {
            return this.View();
        }
    }
}
