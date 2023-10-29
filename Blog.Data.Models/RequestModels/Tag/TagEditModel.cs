namespace Blog.Data.Models.RequestModels.Tag
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class TagEditModel
    {
        [StringLength(AttributesParams.TagValueMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.TagValueMinLength)]
        public string? Value { get; set; }
    }
}
