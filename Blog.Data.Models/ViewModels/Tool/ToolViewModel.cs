using Blog.Data.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models.ViewModels.Tool
{
    public class ToolViewModel
    {
        public ToolViewModel()
        {
            this.PricingStrategies = new HashSet<string>();
        }

        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public bool TopPick { get; set; }
        public bool SpecialOffer { get; set; }
        public bool GPTs { get; set; }
        public string? Creator { get; set; }
        public string? CreationDate { get; set; }
        public string? LastModifier { get; set; }
        public string? LastModifiedOn { get; set; }
        public int FavoriteByUsers { get; set; }
        public bool IsFavorite { get; set; }
        public ICollection<string> PricingStrategies { get; set; }
    }
}
