namespace Blog.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class AssignedTagsViewComponent : ViewComponent
    {
        private readonly IToolService _toolService;

        public AssignedTagsViewComponent(IToolService toolService)
        {
            _toolService = toolService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? toolId)
        {
            var assignedTagsViewModel = await _toolService.GetToolAssignedTagsAsync(toolId);

            return View(assignedTagsViewModel);
        }
    }
}
