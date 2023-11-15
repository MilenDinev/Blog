namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ToolsNewsLettersConfig : IEntityTypeConfiguration<ToolsNewsLetters>
    {
        public void Configure(EntityTypeBuilder<ToolsNewsLetters> builder)
        {
            builder.HasOne(x => x.Tool)
                .WithMany(x => x.NewsLetters)
                .HasForeignKey(x => x.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.NewsLetter)
                .WithMany(x => x.Tools)
                .HasForeignKey(x => x.NewsLetterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ToolId, x.NewsLetterId });
        }
    }
}
