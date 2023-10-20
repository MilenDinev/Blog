namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Entities;

    internal class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity<Dictionary<string, object>>("ArticlesTags",
            a => a.HasOne<Tag>().WithMany().HasForeignKey("TagId")
                .OnDelete(DeleteBehavior.Cascade),
            t => t.HasOne<Article>().WithMany().HasForeignKey("ArticleId")
                .OnDelete(DeleteBehavior.Restrict));

            builder.HasMany(a => a.FavoriteByUsers)
                    .WithMany(u => u.FavoriteArticles)
                    .UsingEntity<Dictionary<string, object>>("UsersFavoriteArticles",
                a => a.HasOne<User>().WithMany().HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                u => u.HasOne<Article>().WithMany().HasForeignKey("ArticleId")
                    .OnDelete(DeleteBehavior.Restrict));

            builder.HasMany(a => a.LikedByUsers)
            .WithMany(u => u.LikedArticles)
                .UsingEntity<Dictionary<string, object>>("UsersLikedArticles",
                a => a.HasOne<User>().WithMany().HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                u => u.HasOne<Article>().WithMany().HasForeignKey("ArticleId")
                    .OnDelete(DeleteBehavior.Restrict));

            builder.HasOne(a => a.Creator)
                .WithMany(u => u.CreatedArticles)
                .HasForeignKey(a => a.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.LastModifier)
                .WithMany(u => u.ModifiedArticles)
                .HasForeignKey(a => a.LastModifierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
