using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRockConcerts.Web.Controllers
{
    public class GroupsController : BaseController
    {
        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}
