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
            .WithMany()
            .HasForeignKey(t => t.CreatorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.LastModifier)
            .WithMany()
            .HasForeignKey(t => t.LastModifierId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
