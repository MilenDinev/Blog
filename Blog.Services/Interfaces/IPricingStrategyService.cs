namespace Blog.Services.Interfaces
{
    using Data.Models.RequestModels.PricingStrategy;

    public interface IPricingStrategyService
    {
        Task CreateAsync(PricingStrategyCreateModel pricingStrategyModel, string userId);
        Task EditAsync(PricingStrategyEditModel pricingStrategyModel, string pricingStrategyId, string modifierId);
        Task DeleteAsync(string pricingStrategyId, string modifierId);
        Task<bool> AnyByIdAsync(string id);
    }
}
