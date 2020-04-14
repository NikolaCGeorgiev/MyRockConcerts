namespace MyRockConcerts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Common;
    using MyRockConcerts.Services.Data;
    using MyRockConcerts.Web.Infrastructure;
    using MyRockConcerts.Web.ViewModels.Genres;
    using MyRockConcerts.Web.ViewModels.Groups;

    public class GroupsController : BaseController
    {
        private readonly IMembersService membersService;
        private readonly IAlbumsService albumsService;
        private readonly IGenresService genresService;
        private readonly IGroupsService groupsService;

        public GroupsController(
            IMembersService membersService,
            IAlbumsService albumsService,
            IGenresService genresService,
            IGroupsService groupsService)
        {
            this.membersService = membersService;
            this.albumsService = albumsService;
            this.genresService = genresService;
            this.groupsService = groupsService;
        }

        [Authorize]
        public async Task<IActionResult> All(int? pageNumber)
        {
            var viewModel = this.groupsService.GetAll<GroupInfoViewModel>();

            return this.View(await PaginatedList<GroupInfoViewModel>
                .CreateAsync(viewModel, pageNumber ?? GlobalConstants.DefaultPageNumber, GlobalConstants.PageSize));
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            // var members = await this.membersService.GetMembersByGroupIdAsync<MemberDetailsViewModel>(id);
            // var albums = await this.albumsService.GetAlbumsByGroupIdAsync<AlbumDetailsViewModel>(id);
            var genres = await this.genresService.GetGenresByGroupIdAsync<GenreViewModel>(id);
            var group = await this.groupsService.GetGroupByIdAsync<GroupDetailsViewModel>(id);

            if (group == null)
            {
                return this.NotFound();
            }

            group.Genres = genres;

            return this.View(group);
        }
    }
}
