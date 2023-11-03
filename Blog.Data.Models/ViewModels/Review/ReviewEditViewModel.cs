namespace Blog.Data.Models.ViewModels.Review
{
    using ViewModels.Tag;
    using ViewModels.PricingStrategy;

    public class ReviewEditViewModel
    {
        public ReviewEditViewModel()
        {
            this.AssignedTags = new HashSet<string>();
            this.AvailableTags = new HashSet<TagViewModel>();
            this.AssignedPricingStrategies = new HashSet<string>();
            this.AvailablePricingStrategies = new HashSet<PricingStrategyViewModel>();
        }

        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public bool TopPick { get; set; }
        public bool SpecialOffer { get; set; }
        public ICollection<string> AssignedTags { get; set; }
        public ICollection<TagViewModel> AvailableTags { get; set; }
        public ICollection<string> AssignedPricingStrategies { get; set; }
        public ICollection<PricingStrategyViewModel> AvailablePricingStrategies { get; set; }
    }
}
