namespace Blog.Web.ViewComponents
{
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AvailablePricingStrategiesViewComponent : ViewComponent
    {
        private readonly IPricingStrategyService _pricingStrategyService;

        public AvailablePricingStrategiesViewComponent(IPricingStrategyService pricingStrategyService)
        {
            _pricingStrategyService = pricingStrategyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var assignedTagsViewModel = await _pricingStrategyService.GetPricingStrategyViewModelBundleAsync();

            return View(assignedTagsViewModel);
        }
    }
}
