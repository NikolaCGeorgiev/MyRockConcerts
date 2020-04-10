namespace MyRockConcerts.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRockConcerts.Data.Models;

    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder
                .HasKey(ug => new { ug.UserId, ug.GroupId });
        }
    }
}
