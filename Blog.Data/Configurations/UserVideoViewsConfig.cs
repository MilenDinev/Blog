namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class UserVideoViewsConfig : IEntityTypeConfiguration<UserVideoViews>
    {
        public void Configure(EntityTypeBuilder<UserVideoViews> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(t => t.UserVideosViewed)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Video)
                .WithMany(t => t.VideoUsersViews)
                .HasForeignKey(a => a.VideoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
