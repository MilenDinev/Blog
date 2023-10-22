namespace Blog.Data.Entities
{
    using Interfaces;

    public class Article : IEntity
    {
        public Article()
        {
            this.FavoriteByUsers = new HashSet<User>();
            this.Tags= new HashSet<Tag>();
            this.PricingStrategies = new HashSet<PricingStrategy>();
            this.UpVotes = new HashSet<UpVote>();
            this.DownVotes = new HashSet<DownVote>();
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
        public virtual ICollection<UpVote> UpVotes { get; set; }
        public virtual ICollection<DownVote> DownVotes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
        public virtual ICollection<User> LikedByUsers { get; set; }
        public virtual ICollection<PricingStrategy> PricingStrategies { get; set; }

    }
}
