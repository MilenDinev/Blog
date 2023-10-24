namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class UserArticleViewsConfig : IEntityTypeConfiguration<UserArticleViews>
    {
        public void Configure(EntityTypeBuilder<UserArticleViews> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(t => t.UserArticlesViewed)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Article)
                .WithMany(t => t.ArticleUsersViews)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
