namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
