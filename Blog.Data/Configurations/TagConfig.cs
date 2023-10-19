namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities;

    internal class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasOne(t => t.Creator)
            .WithMany(u => u.CreatedTags)
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.LastModifier)
            .WithMany(u => u.ModifiedTags)
            .HasForeignKey(t => t.LastModifierId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
