namespace MyRockConcerts.Web.ViewModels.Groups
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class GroupTestVewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
