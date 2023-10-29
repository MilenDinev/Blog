namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    internal class VideoConfig : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Videos)
                .UsingEntity<Dictionary<string, object>>("VideosTags",
            a => a.HasOne<Tag>().WithMany().HasForeignKey("TagId")
                .OnDelete(DeleteBehavior.Cascade),
            t => t.HasOne<Video>().WithMany().HasForeignKey("VideoId")
                .OnDelete(DeleteBehavior.Restrict));

            builder.HasOne(p => p.Creator)
                .WithMany()
                .HasForeignKey(p => p.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.LastModifier)
                .WithMany()
                .HasForeignKey(p => p.LastModifierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
