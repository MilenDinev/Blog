namespace Blog.Data.Configurations.Shared
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities.Shared;

    internal class NewsLettersSubscribersConfig : IEntityTypeConfiguration<NewsLettersSubscribers>
    {
        public void Configure(EntityTypeBuilder<NewsLettersSubscribers> builder)
        {
            builder.HasOne(x => x.NewsLetter)
                .WithMany(x => x.Subscribers)
                .HasForeignKey(x => x.NewsLetterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Subscriber)
                .WithMany(x => x.NewsLetters)
                .HasForeignKey(x => x.SubscriberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(x => new { x.NewsLetterId, x.SubscriberId });
        }
    }
}
