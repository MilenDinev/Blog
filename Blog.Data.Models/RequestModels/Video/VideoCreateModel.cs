namespace Blog.Data.Models.RequestModels.Video
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class VideoCreateModel
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.VideoTitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.VideoTitleMinLength)]
        public string? Title { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? Url { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ImageUrl { get; set; }

        [StringLength(AttributesParams.RelatedToolNameMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.RelatedToolNameMinLength)]
        public string? RelatedToolName { get; set; }

        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? RelatedToolUrl { get; set; }
    }
}
