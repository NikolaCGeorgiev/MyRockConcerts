namespace MyRockConcerts.Web.Controllers
{
    using System.Security.Claims;
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
        private readonly IGenresService genresService;
        private readonly IGroupsService groupsService;

        public GroupsController(
            IGenresService genresService,
            IGroupsService groupsService)
        {
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
        public async Task<IActionResult> MyGroups(int? pageNumber)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.groupsService.GetAll<GroupInfoViewModel>(userId);

            return this.View(await PaginatedList<GroupInfoViewModel>
                .CreateAsync(viewModel, pageNumber ?? GlobalConstants.DefaultPageNumber, GlobalConstants.PageSize));
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            // var albums = await this.albumsService.GetAlbumsByGroupIdAsync<AlbumDetailsViewModel>(id);
            var group = await this.groupsService.GetGroupByIdAsync<GroupDetailsViewModel>(id);

            if (group == null)
            {
                return this.NotFound();
            }

            var genres = await this.genresService.GetGenresByGroupIdAsync<GenreViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            group.Genres = genres;
            group.IsMyFavorite = await this.groupsService.IsMyFavoriteAsync(id, userId);

            return this.View(group);
        }

        [Authorize]
        public async Task<IActionResult> ByGenre(int? pageNumber, int id)
        {
            var groupsInfo = this.groupsService.GetGroupsByGenreId<GroupInfoViewModel>(id);
            var groups = await PaginatedList<GroupInfoViewModel>
                .CreateAsync(groupsInfo, pageNumber ?? GlobalConstants.DefaultPageNumber, GlobalConstants.PageSize);
            var genreName = await this.genresService.GetNameByIdAsync(id);

            var viewModel = new GroupsListViewModel
            {
                GenreName = genreName,
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Follow(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.groupsService.AddToMyFavoritesAsync(id, userId);

            return this.Redirect("/Groups/MyGroups");
        }

        [Authorize]
        public async Task<IActionResult> Unfollow(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.groupsService.RemoveFromMyFavoritesAsync(id, userId);

            return this.Redirect("/Groups/MyGroups");
        }
    }
}
