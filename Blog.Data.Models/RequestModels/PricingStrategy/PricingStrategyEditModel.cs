﻿namespace Blog.Data.Models.RequestModels.PricingStrategy
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class PricingStrategyEditModel
    {
        [StringLength(AttributesParams.StrategyMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.StrategyMinLength)]
        public string? Strategy { get; set; }
    }
}
