﻿namespace MyRockConcerts.Web.ViewModels.Members
{
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class MemberInfoViewModel : IMapFrom<Member>, IMapTo<Member>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public Group Group { get; set; }
    }
}
