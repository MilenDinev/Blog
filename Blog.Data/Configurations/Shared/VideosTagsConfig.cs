namespace Blog.Data.Configurations.Shared
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class VideosTagsConfig : IEntityTypeConfiguration<VideosTags>
    {
        public void Configure(EntityTypeBuilder<VideosTags> builder)
        {
            builder.HasOne(x => x.Video)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.Videos)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.VideoId, x.TagId });
        }
    }
}
