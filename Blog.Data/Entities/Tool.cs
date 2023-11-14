namespace Blog.Data.Entities
{
    using Interfaces;
    using Entities.Shared;

    public class Tool : IEntity
    {
        public Tool()
        {
            this.FavoriteByUsers = new HashSet<UsersFavoriteTools>();
            this.Tags= new HashSet<ToolsTags>();
            this.PricingStrategies = new HashSet<ToolsPricingStrategies>();
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
        public virtual ICollection<ToolsTags> Tags { get; set; }
        public virtual ICollection<UsersFavoriteTools> FavoriteByUsers { get; set; }
        public virtual ICollection<ToolsPricingStrategies> PricingStrategies { get; set; }
    }
}
