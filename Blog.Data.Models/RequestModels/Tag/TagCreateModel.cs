namespace Blog.Data.Models.RequestModels.Tag
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class TagCreateModel
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.TagValueMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.TagValueMinLength)]
        public string? Value { get; set; }
    }
}
