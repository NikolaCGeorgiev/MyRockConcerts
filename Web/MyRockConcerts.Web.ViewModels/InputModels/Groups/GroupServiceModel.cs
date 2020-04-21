namespace MyRockConcerts.Web.ViewModels.InputModels.Groups
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupServiceModel : IMapTo<Group>, IMapFrom<Group>
    {
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }
    }
}
