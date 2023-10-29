namespace Blog.Data.Entities
{
    using System;
    using Interfaces;

    public class PricingStrategy : IEntity
    {
        public PricingStrategy()
        {
            this.Reviews = new HashSet<Review>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Model { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
