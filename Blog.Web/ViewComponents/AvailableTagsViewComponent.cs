namespace Blog.Web.ViewComponents
{
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AvailableTagsViewComponent : ViewComponent
    {
        private readonly ITagService _tagService;


        public AvailableTagsViewComponent(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var assignedTagsViewModel = await _tagService.GetTagViewModelBundleAsync();

            return View(assignedTagsViewModel);
        }
    }
}
