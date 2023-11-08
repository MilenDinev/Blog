namespace Blog.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class CreatedReviewsViewComponent : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public CreatedReviewsViewComponent(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            var reviewsCountViewModel =  await _reviewService.GetCreatedReviewsCountAsync(title);

            return View(reviewsCountViewModel);
        }
    }
}
