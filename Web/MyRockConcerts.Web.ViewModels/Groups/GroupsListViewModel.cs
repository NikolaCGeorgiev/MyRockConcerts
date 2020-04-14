namespace MyRockConcerts.Web.ViewModels.Groups
{
    using MyRockConcerts.Web.Infrastructure;

    public class GroupsListViewModel
    {
        public string GenreName { get; set; }

        public PaginatedList<GroupInfoViewModel> Groups { get; set; }
    }
}
