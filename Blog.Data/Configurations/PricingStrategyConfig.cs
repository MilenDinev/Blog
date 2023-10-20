namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities;

    internal class PricingStrategyConfig : IEntityTypeConfiguration<PricingStrategy>
    {
        public void Configure(EntityTypeBuilder<PricingStrategy> builder)
        {
            builder.HasOne(t => t.Creator)
            .WithMany(u => u.CreatedPricingStrategies)
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.LastModifier)
            .WithMany(u => u.ModifiedPricingStrategies)
            .HasForeignKey(t => t.LastModifierId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
