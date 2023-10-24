namespace Blog.Data.Entities
{
    using System;
    using Interfaces;

    public class Article : IEntity
    {
        public Article()
        {
            this.Tags = new HashSet<Tag>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string ProviderName { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
