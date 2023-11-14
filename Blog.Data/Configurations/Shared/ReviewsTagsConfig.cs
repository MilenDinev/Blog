namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ToolsTagsConfig : IEntityTypeConfiguration<ToolsTags>
    {
        public void Configure(EntityTypeBuilder<ToolsTags> builder)
        {
            builder.HasOne(x => x.Tool)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.Tools)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ToolId, x.TagId });
        }
    }
}
