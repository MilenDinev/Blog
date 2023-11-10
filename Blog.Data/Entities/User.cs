namespace Blog.Data.Entities
{
    using Blog.Data.Entities.Shared;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {     
            this.FavoriteReviews = new HashSet<UsersFavoriteReviews>();
            this.Votes = new HashSet<Vote>();
        }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<UsersFavoriteReviews> FavoriteReviews { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
