namespace MyRockConcerts.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRockConcerts.Data.Models;

    public class ConcertGroupConfiguration : IEntityTypeConfiguration<ConcertGroup>
    {
        public void Configure(EntityTypeBuilder<ConcertGroup> builder)
        {
            builder
                .HasKey(cg => new { cg.ConcertId, cg.GroupId });
        }
    }
}
