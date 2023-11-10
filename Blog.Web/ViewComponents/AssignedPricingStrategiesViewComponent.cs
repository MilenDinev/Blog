namespace Blog.Web.ViewComponents
{
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class AssignedPricingStrategiesViewComponent : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public AssignedPricingStrategiesViewComponent(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? reviewId)
        {
            var assignedTagsViewModel = await _reviewService.GetReviewAssignedPricingStrategiesAsync(reviewId);

            return View(assignedTagsViewModel);
        }
    }
}
