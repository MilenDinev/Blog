﻿namespace Blog.Services.Interfaces
{
    using Data.ViewModels.PricingStrategy;
    using Data.Models.RequestModels.PricingStrategy;

    public interface IPricingStrategyService
    {
        Task CreateAsync(PricingStrategyCreateModel pricingStrategyModel, string userId);
        Task<ICollection<PricingStrategyViewModel>> GetPricingStrategyViewModelBundleAsync();
    }
}
