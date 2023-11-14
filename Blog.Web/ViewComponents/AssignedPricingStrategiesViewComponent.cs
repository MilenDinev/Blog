namespace Blog.Web.ViewComponents
{
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AssignedPricingStrategiesViewComponent : ViewComponent
    {
        private readonly IToolService _toolService;

        public AssignedPricingStrategiesViewComponent(IToolService toolService)
        {
            _toolService = toolService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? toolId)
        {
            var assignedTagsViewModel = await _toolService.GetToolAssignedPricingStrategiesAsync(toolId);

            return View(assignedTagsViewModel);
        }
    }
}
