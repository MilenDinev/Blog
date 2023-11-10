namespace Blog.Web.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.Models.RequestModels.Review;
    using Services.Interfaces;

    [Route("reviews")]
    public class ReviewsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService,
            IUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        private string? CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var reviewViewModel = await _reviewService.GetReviewViewModelByIdAsync(id);

            if (CurrentUserId is not null)
                reviewViewModel.IsFavorite = await _userService.IsFavoriteReviewAsync(CurrentUserId, id);
                 
            return View(reviewViewModel);
        }

        [HttpGet("Latest")]
        public async Task<IActionResult> Latest(string? search)
        {
            var reviewsPreviewModelBundle = await _reviewService.GetTodaysReviewPreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
                reviewsPreviewModelBundle = reviewsPreviewModelBundle
                    .Where(review => review.Title.Contains(search))
                    .ToList();

            return View(reviewsPreviewModelBundle);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {
            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (ModelState.IsValid)
            {
                await _reviewService.CreateAsync(reviewCreateModel, CurrentUserId);

                return RedirectToAction("Index", "Home");
            }

            return View(reviewCreateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return BadRequest();

            var reviewEditViewModel = await _reviewService.GetReviewEditViewModelByIdAsync(id);

            if (reviewEditViewModel == null)
                return NotFound();

            return View(reviewEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(ReviewEditModel reviewEditModel, string? id)
        {
            if (id == null)
                return BadRequest();

            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            try
            {

                await _reviewService.EditAsync(reviewEditModel, id, CurrentUserId);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            var reviewEditViewModel = await _reviewService.GetReviewEditViewModelByIdAsync(id);

            return View(reviewEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
                return BadRequest();

            var reviewDeleteViewModel = await _reviewService.GetReviewDeleteViewModelByIdAsync(id);

            return View(reviewDeleteViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteReview(string? id)
        {
            if (id == null)
                return BadRequest();

            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            try
            {
                await _reviewService.DeleteAsync(id, CurrentUserId);

               return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}