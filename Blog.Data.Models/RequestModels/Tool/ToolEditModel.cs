namespace Blog.Data.Models.RequestModels.Tool
{
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using ViewModels.Tag;
    using ViewModels.PricingStrategy;

    public class ToolEditModel
    {
        public ToolEditModel()
        {
            this.AssignedTags = new HashSet<string>();
            this.AvailableTags = new HashSet<TagViewModel>();
            this.AssignedPricingStrategies = new HashSet<string>();
            this.AvailablePricingStrategies = new HashSet<PricingStrategyViewModel>();
        }

        [StringLength(AttributesParams.ToolTitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.ToolTitleMinLength)]
        public string? Title { get; set; }

        [StringLength(AttributesParams.DescriptionMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.DescriptionMinLength)]
        public string? Description { get; set; }

        [MinLength(AttributesParams.ContentMinLength,
            ErrorMessage = ValidationMessages.MinMaxLength)]
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

        [Required(ErrorMessage = ValidationMessages.Required)]
        public bool GPTs { get; set; }

        public ICollection<string> AssignedTags { get; set; }
        public ICollection<TagViewModel> AvailableTags { get; set; }
        public ICollection<string> AssignedPricingStrategies { get; set; }
        public ICollection<PricingStrategyViewModel> AvailablePricingStrategies { get; set; }
    }
}
