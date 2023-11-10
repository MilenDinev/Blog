namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class ArticlesTagsConfig : IEntityTypeConfiguration<ArticlesTags>
    {
        public void Configure(EntityTypeBuilder<ArticlesTags> builder)
        {
            builder.HasOne(x => x.Article)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.ArticleId, x.TagId });
        }
    }
}
