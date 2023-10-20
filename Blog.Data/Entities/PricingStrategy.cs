namespace Blog.Data.Entities
{
    using Blog.Data.Entities.Interfaces;
    using System;

    public class PricingStrategy : IEntity
    {
        public PricingStrategy()
        {
                this.Articles = new HashSet<Article>();
        }

        public string Id { get; set; }
        public string Model { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
