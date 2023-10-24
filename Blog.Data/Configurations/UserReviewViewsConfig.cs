namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class UserReviewViewsConfig : IEntityTypeConfiguration<UserReviewViews>
    {
        public void Configure(EntityTypeBuilder<UserReviewViews> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(t => t.UserReviewsViewed)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Review)
                .WithMany(t => t.ReviewUsersViews)
                .HasForeignKey(a => a.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}