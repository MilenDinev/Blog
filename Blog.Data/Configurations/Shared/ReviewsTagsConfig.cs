namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ReviewsTagsConfig : IEntityTypeConfiguration<ReviewsTags>
    {
        public void Configure(EntityTypeBuilder<ReviewsTags> builder)
        {
            builder.HasOne(x => x.Review)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ReviewId, x.TagId });
        }
    }
}
