namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GroupsController : AdministrationController
    {
        public IActionResult Create()
        {
            return this.View();
        }
    }
}
