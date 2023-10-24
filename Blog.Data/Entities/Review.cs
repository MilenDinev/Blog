namespace Blog.Data.Entities
{
    using Interfaces;

    public class Review : IEntity
    {
        public Review()
        {
            this.FavoriteByUsers = new HashSet<User>();
            this.Tags= new HashSet<Tag>();
            this.PricingStrategies = new HashSet<PricingStrategy>();
            this.Votes = new HashSet<Vote>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public string NormalizedTag { get; set; }
        public bool TopPick { get; set; }
        public bool SpecialOffer { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public bool Deleted { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
        public virtual ICollection<PricingStrategy> PricingStrategies { get; set; }
    }
}
