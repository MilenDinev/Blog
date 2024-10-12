namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ToolsPricingStrategiesConfig : IEntityTypeConfiguration<ToolsPricingStrategies>
    {
        public void Configure(EntityTypeBuilder<ToolsPricingStrategies> builder)
        {
            builder.HasOne(x => x.Tool)
                .WithMany(x => x.PricingStrategies)
                .HasForeignKey(x => x.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PricingStrategy)
                .WithMany(x => x.Tools)
                .HasForeignKey(x => x.PricingStrategyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ToolId, x.PricingStrategyId });
        }
    }
}
