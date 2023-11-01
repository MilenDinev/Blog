namespace Blog.Data.Models.RequestModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using ViewModels.Tag;
    using ViewModels.PricingStrategy;

    public class ReviewCreateModel
    {
        public ReviewCreateModel()
        {
            this.AssignedTags = new HashSet<string>();
            this.AvailableTags = new HashSet<TagViewModel>();
            this.AssignedPricingStrategies = new HashSet<string>();
            this.AvailablePricingStrategies = new HashSet<PricingStrategyViewModel>();
        }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.TitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.TitleMinLength)]
        public string? Title { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.DescriptionMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.DescriptionMinLength)]
        public string? Description { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MinLength(AttributesParams.ContentMinLength,
            ErrorMessage = ValidationMessages.MinLength)]
        public string? Content { get; set; }

        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ImageUrl { get; set; }
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? VideoUrl { get; set; }
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ExternalArticleUrl { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool TopPick { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool SpecialOffer { get; set; }
        public ICollection<string> AssignedTags { get; set; }
        public ICollection<TagViewModel> AvailableTags { get; set; }
        public ICollection<string> AssignedPricingStrategies { get; set; }
        public ICollection<PricingStrategyViewModel> AvailablePricingStrategies { get; set; }
    }
}
