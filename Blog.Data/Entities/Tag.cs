namespace Blog.Data.Entities
{
    using Interfaces;

    public class Tag : IEntity
    {
        public Tag()
        {
            this.Reviews = new HashSet<Review>();
            this.Articles = new HashSet<Article>();
            this.Videos = new HashSet<Video>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Value { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
