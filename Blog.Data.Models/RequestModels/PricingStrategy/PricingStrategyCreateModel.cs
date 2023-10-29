namespace Blog.Data.Models.RequestModels.PricingStrategy
{
    using Constants;
    using System.ComponentModel.DataAnnotations;

    public class PricingStrategyCreateModel
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(AttributesParams.StrategyMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.StrategyMinLength)]
        public string? Model { get; set; }
    }
}
