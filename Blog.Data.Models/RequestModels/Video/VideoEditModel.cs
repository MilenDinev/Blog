namespace Blog.Data.Models.RequestModels.Video
{
    using Constants;
    using System.ComponentModel.DataAnnotations;

    public class VideoEditModel
    {
        [StringLength(AttributesParams.VideoTitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.VideoTitleMinLength)]
        public string? Title { get; set; }

        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? Url { get; set; }

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
