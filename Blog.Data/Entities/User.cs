namespace Blog.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            this.CreatedArticles = new HashSet<Article>();
            this.ModifiedArticles = new HashSet<Article>();
            this.FavoriteArticles = new HashSet<Article>();
            this.LikedArticles = new HashSet<Article>();
            this.CreatedTags = new HashSet<Tag>();
            this.ModifiedTags = new HashSet<Tag>();
            this.CreatedPricingStrategies = new HashSet<PricingStrategy>();
            this.ModifiedPricingStrategies = new HashSet<PricingStrategy>();
            this.UpVotes = new HashSet<UpVote>();
            this.DownVotes = new HashSet<DownVote>();
        }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Article> CreatedArticles { get; set; }
        public virtual ICollection<Article> ModifiedArticles { get; set; }
        public virtual ICollection<PricingStrategy> CreatedPricingStrategies { get; set; }
        public virtual ICollection<PricingStrategy> ModifiedPricingStrategies { get; set; }
        public virtual ICollection<Article> FavoriteArticles { get; set; }
        public virtual ICollection<Article> LikedArticles { get; set; }
        public virtual ICollection<Tag> CreatedTags { get; set; }
        public virtual ICollection<Tag> ModifiedTags{ get; set; }
        public virtual ICollection<UpVote> UpVotes { get; set; }
        public virtual ICollection<DownVote> DownVotes { get; set; }
    }
}
