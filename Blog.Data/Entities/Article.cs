﻿namespace Blog.Data.Entities
{
    using Interfaces;

    public class Article : IEntity
    {
        public Article()
        {
            this.FavoriteByUsers = new HashSet<User>();
            this.Tags= new HashSet<Tag>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string? LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<User> FavoriteByUsers { get; set; }
        public virtual ICollection<User> LikedByUsers { get; set; }
        public bool Deleted { get; set; }
    }
}
