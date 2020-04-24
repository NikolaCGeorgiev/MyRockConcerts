namespace MyRockConcerts.Services.Data.Tests.Common
{
    using System.Reflection;

    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.Groups;
    using MyRockConcerts.Web.ViewModels.Members;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(MemberTestVewModel).GetTypeInfo().Assembly,
                typeof(GroupTestVewModel).GetTypeInfo().Assembly);
        }
    }
}
