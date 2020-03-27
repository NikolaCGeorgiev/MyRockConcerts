namespace MyRockConcerts.Web.Areas.Administration.Controllers
{
    using MyRockConcerts.Common;
    using MyRockConcerts.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
