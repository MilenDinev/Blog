namespace Blog.Data.Entities
{
    using System;
    using Shared;
    using Interfaces;

    public class Video : IEntity
    {
        public Video()
        {
            this.CreationDate = DateTime.UtcNow;
            this.LastModifiedOn = DateTime.UtcNow;  

            this.VideoUsersViews = new HashSet<UserVideoViews>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NormalizedTag { get; set; }
        public bool Deleted { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastModifierId { get; set; }
        public virtual User LastModifier { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public virtual ICollection<UserVideoViews> VideoUsersViews { get; set; }
    }
}
