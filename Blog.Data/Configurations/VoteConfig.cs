namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    internal class VoteConfig : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(a => a.Review)
                .WithMany(u => u.Votes)
                .HasForeignKey(a => a.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(a => a.Type)
                .IsRequired();
        }
    }
}