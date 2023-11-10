namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ReviewsPricingStrategiesConfig : IEntityTypeConfiguration<ReviewsPricingStrategies>
    {
        public void Configure(EntityTypeBuilder<ReviewsPricingStrategies> builder)
        {
            builder.HasOne(x => x.Review)
                .WithMany(x => x.PricingStrategies)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PricingStrategy)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.PricingStrategyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ReviewId, x.PricingStrategyId });
        }
    }
}
