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
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.Id, a.ReviewId, a.UserId }).IsUnique();

            builder.Property(a => a.Type)
                .IsRequired();
        }
    }
}