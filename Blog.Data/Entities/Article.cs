﻿namespace Blog.Data.Entities
{
    using System;
    using Shared;
    using Interfaces;

    public class Article : IEntity
    {
        public Article()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tags = new HashSet<ArticlesTags>();
        }

        public string Id { get; set; }
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
        public virtual ICollection<ArticlesTags> Tags { get; set; }
    }
}
