namespace Blog.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class AssignedTagsViewComponent : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public AssignedTagsViewComponent(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? reviewId)
        {
            var assignedTagsViewModel = await _reviewService.GetReviewAssignedTagsAsync(reviewId);

            return View(assignedTagsViewModel);
        }
    }
}
