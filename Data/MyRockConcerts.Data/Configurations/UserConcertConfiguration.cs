namespace MyRockConcerts.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyRockConcerts.Data.Models;

    public class UserConcertConfiguration : IEntityTypeConfiguration<UserConcert>
    {
        public void Configure(EntityTypeBuilder<UserConcert> builder)
        {
            builder
                .HasKey(uc => new { uc.UserId, uc.ConcertId });
        }
    }
}
