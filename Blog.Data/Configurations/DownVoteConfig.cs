namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;

    internal class DownVoteConfig : IEntityTypeConfiguration<DownVote>
    {
        public void Configure(EntityTypeBuilder<DownVote> builder)
        {
            builder.HasOne(a => a.Article)
                .WithMany(u => u.DownVotes)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(a => a.User)
                .WithMany(u => u.DownVotes)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
