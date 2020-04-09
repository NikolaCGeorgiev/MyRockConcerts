namespace MyRockConcerts.Web.ViewModels.Members
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class MemberDetailsViewModel : IMapFrom<Member>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
