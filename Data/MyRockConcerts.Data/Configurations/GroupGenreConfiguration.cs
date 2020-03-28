namespace MyRockConcerts.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRockConcerts.Data.Models;

    public class GroupGenreConfiguration : IEntityTypeConfiguration<GroupGenre>
    {
        public void Configure(EntityTypeBuilder<GroupGenre> builder)
        {
            builder
                .HasKey(gg => new { gg.GroupId, gg.GenreId });
        }
    }
}
