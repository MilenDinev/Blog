namespace Blog.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class CreatedToolsViewComponent : ViewComponent
    {
        private readonly IToolService _toolService;

        public CreatedToolsViewComponent(IToolService toolService)
        {
            _toolService = toolService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            var toolsCountViewModel =  await _toolService.GetCreatedToolsCountAsync(title);

            return View(toolsCountViewModel);
        }
    }
}
