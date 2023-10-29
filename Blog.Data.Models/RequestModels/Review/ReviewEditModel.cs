namespace Blog.Data.Models.RequestModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class ReviewEditModel
    {
        [StringLength(AttributesParams.TitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.TitleMinLength)]
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
        public bool? TopPick { get; set; }
        public bool? SpecialOffer { get; set; }
        //public ICollection<string> Tags { get; set; }
        //public ICollection<string> PricingStrategies { get; set; }
    }
}
