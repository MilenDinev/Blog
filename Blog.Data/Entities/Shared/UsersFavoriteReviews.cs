using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities.Shared
{
    public class UsersFavoriteReviews
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }
}
