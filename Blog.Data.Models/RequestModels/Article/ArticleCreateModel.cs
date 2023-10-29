namespace Blog.Data.Models.RequestModels.Article
{
    using System.ComponentModel.DataAnnotations;
    using Constants;
    public class ArticleCreateModel
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.TitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.TitleMinLength)]
        public string? Title { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? Url { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.ProviderMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.ProviderMinLength)]
        public string? ProviderName { get; set; }
    }
}
