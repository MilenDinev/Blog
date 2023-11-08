namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities;

    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Reviews)
                .UsingEntity<Dictionary<string, object>>("ReviewsTags",
            a => a.HasOne<Tag>().WithMany().HasForeignKey("TagId")
                .OnDelete(DeleteBehavior.Cascade),
            t => t.HasOne<Review>().WithMany().HasForeignKey("ReviewId")
                .OnDelete(DeleteBehavior.Restrict));


                builder.HasMany(a => a.FavoriteByUsers)
                    .WithMany(u => u.FavoriteReviews)
                    .UsingEntity<UsersFavoriteReviews>(
                a => a.HasOne<User>().WithMany().HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict),
                u => u.HasOne<Review>().WithMany().HasForeignKey("ReviewId")
                    .OnDelete(DeleteBehavior.Restrict));

            builder.HasOne(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.LastModifier)
                .WithMany()
                .HasForeignKey(a => a.LastModifierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
