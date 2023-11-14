using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities.Shared
{
    public class UsersFavoriteTools
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ToolId { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
