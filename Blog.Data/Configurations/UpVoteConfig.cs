using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    internal class UpVoteConfig : IEntityTypeConfiguration<UpVote>
    {
        public void Configure(EntityTypeBuilder<UpVote> builder)
        {
            builder.HasOne(a => a.Article)
                .WithMany(u => u.UpVotes)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithMany(u => u.UpVotes)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}