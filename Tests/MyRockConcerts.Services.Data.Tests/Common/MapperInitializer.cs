namespace MyRockConcerts.Services.Data.Tests.Common
{
    using System.Reflection;

    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Members;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Member).GetTypeInfo().Assembly,
                typeof(MemberTestVewModel).GetTypeInfo().Assembly);
        }
    }
}
