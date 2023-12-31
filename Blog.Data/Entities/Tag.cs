﻿namespace Blog.Data.Entities
{
    using Blog.Data.Entities.Shared;
    using Interfaces;

    public class Tag : IEntity
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tools = new HashSet<ToolsTags>();
            this.Articles = new HashSet<ArticlesTags>();
            this.Videos = new HashSet<VideosTags>();
        }

        public string Id { get; set; }
        public string Value { get; set; }
        public string NormalizedTag { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<ToolsTags> Tools { get; set; }
        public virtual ICollection<ArticlesTags> Articles { get; set; }
        public virtual ICollection<VideosTags> Videos { get; set; }
    }
}
