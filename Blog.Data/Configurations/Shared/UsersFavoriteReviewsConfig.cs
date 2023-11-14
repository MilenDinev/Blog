namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities.Shared;

    internal class UsersFavoriteToolsConfigConfig : IEntityTypeConfiguration<UsersFavoriteTools>
    {
        public void Configure(EntityTypeBuilder<UsersFavoriteTools> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.FavoriteTools)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tool)
                .WithMany(x => x.FavoriteByUsers)
                .HasForeignKey(x => x.ToolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.UserId, x.ToolId });
        }
    }
}