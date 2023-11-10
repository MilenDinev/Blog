namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities.Shared;

    internal class UsersFavoriteReviewsConfigConfig : IEntityTypeConfiguration<UsersFavoriteReviews>
    {
        public void Configure(EntityTypeBuilder<UsersFavoriteReviews> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.FavoriteReviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Review)
                .WithMany(x => x.FavoriteByUsers)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.UserId, x.ReviewId });
        }
    }
}