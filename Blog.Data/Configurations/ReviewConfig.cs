namespace Blog.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entities;
    using Microsoft.Data.SqlClient;

    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            builder.HasOne(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.LastModifier)
                .WithMany()
                .HasForeignKey(a => a.LastModifierId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.CreationDate);
        }
    }
}
