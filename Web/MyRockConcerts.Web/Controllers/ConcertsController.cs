using Microsoft.AspNetCore.Mvc;
using MyRockConcerts.Services.Data;
using MyRockConcerts.Web.ViewModels.Groups;
using MyRockConcerts.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyRockConcerts.Web.ViewModels.Concerts;
using MyRockConcerts.Web.ViewModels;

namespace MyRockConcerts.Web.Controllers
{
    public class ConcertsController : BaseController
    {
        private readonly IConcertsService concertsService;
        private readonly IGroupsService groupsService;

        public ConcertsController(IConcertsService concertsService, IGroupsService groupsService)
        {
            this.concertsService = concertsService;
            this.groupsService = groupsService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var groups = await this.groupsService.GetGroupsByConcertId<GroupInfoViewModel>(id);
            var concert = await this.concertsService.GetByIdAsync<ConcertViewModel>(id);

            if (concert == null)
            {
                return this.NotFound();
            }

            concert.Groups = groups;

            return this.View(concert);
        }
    }
}
