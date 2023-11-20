namespace Blog.Data.Models.RequestModels.Tool
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;
    using ViewModels.Tag;
    using ViewModels.PricingStrategy;

    public class ToolCreateModel
    {
        public ToolCreateModel()
        {
            this.AssignedTags = new HashSet<string>();
            this.AvailableTags = new HashSet<TagViewModel>();
            this.AssignedPricingStrategies = new HashSet<string>();
            this.AvailablePricingStrategies = new HashSet<PricingStrategyViewModel>();
        }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.ToolTitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.ToolTitleMinLength)]
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

        [Display(Name = "Image Url")]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Video Url")]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? VideoUrl { get; set; }

        [Display(Name = "External Url")]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ExternalArticleUrl { get; set; }

        [Display(Name = "Top Pick")]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool TopPick { get; set; }

        [Display(Name = "Special Offer")]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool SpecialOffer { get; set; }

        [Display(Name = "Is GPTs")]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool GPTs { get; set; }

        public ICollection<string> AssignedTags { get; set; }
        public ICollection<TagViewModel> AvailableTags { get; set; }
        public ICollection<string> AssignedPricingStrategies { get; set; }
        public ICollection<PricingStrategyViewModel> AvailablePricingStrategies { get; set; }
    }
}
