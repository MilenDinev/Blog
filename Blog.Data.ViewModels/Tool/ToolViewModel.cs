namespace Blog.Data.ViewModels.Tool
{
    using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Video Url")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Video Url")]
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        [Display(Name = "Top Pick")]
        public bool TopPick { get; set; }
        [Display(Name = "Special Offer")]
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
