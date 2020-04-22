namespace MyRockConcerts.Web.ViewModels.Members
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class MemberTestVewModel : IMapFrom<Member>
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
