namespace Blog.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class User : IdentityUser
    {
        public User()
        {
            CreatedArticles = new HashSet<Article>();
            ModifiedArticles = new HashSet<Article>();
            FavoriteArticles = new HashSet<Article>();
            LikedArticles = new HashSet<Article>();
            CreatedTags = new HashSet<Tag>();
            ModifiedTags = new HashSet<Tag>();
        }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public virtual ICollection<Article> CreatedArticles { get; set; }
        public virtual ICollection<Article> ModifiedArticles { get; set; }
        public virtual ICollection<Article> FavoriteArticles { get; set; }
        public virtual ICollection<Article> LikedArticles { get; set; }
        public virtual ICollection<Tag> CreatedTags { get; set; }
        public virtual ICollection<Tag> ModifiedTags{ get; set; }
        public bool Deleted { get; set; }
    }
}
