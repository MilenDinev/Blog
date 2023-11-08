namespace Blog.Web.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.Models.RequestModels.Review;
    using Services.Interfaces;

    [Route("Reviews")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ITagService _tagService;
        private readonly IPricingStrategyService _pricingStrategyService;
        private readonly IUserService _userService;

        public ReviewsController(IReviewService reviewService,
            ITagService tagService,
            IPricingStrategyService pricingStrategyService,
            IUserService userService)
        {
            _reviewService = reviewService;
            _tagService = tagService;
            _pricingStrategyService = pricingStrategyService;
            _userService = userService;

        }

        [HttpGet("{id}")]
        [Route("Review/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var reviewViewModel = await _reviewService.GetReviewViewModelByIdAsync(id);
            // increace view//
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

                reviewViewModel.IsFavorite = await _userService.IsFavoriteReviewAsync(userId, id);
            

            return View(reviewViewModel);
        }

        [HttpGet("{search}")]
        [Route("Latest")]
        public async Task<IActionResult> Latest(string? search)
        {
            var reviewsPreviewModelBundle = await _reviewService.GetTodaysReviewPreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
            {
                reviewsPreviewModelBundle = reviewsPreviewModelBundle.Where(review => review.Title.Contains(search)).ToList();

            }

            return View(reviewsPreviewModelBundle);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException();

                await _reviewService.CreateAsync(reviewCreateModel, userId);

                return RedirectToAction("Index", "Home");
            }

            var tagViewModelBundle = await _tagService.GetTagViewModelBundleAsync();
            reviewCreateModel.AvailableTags = tagViewModelBundle;

            var pricingStrategyViewModelBundle = await _pricingStrategyService.GetPricingStrategyViewModelBundleAsync();
            reviewCreateModel.AvailablePricingStrategies = pricingStrategyViewModelBundle;

            return View(reviewCreateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reviewEditViewModel = await _reviewService.GetReviewEditViewModelByIdAsync(id);

            if (reviewEditViewModel == null)
            {
                return NotFound();
            }

            return View(reviewEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(ReviewEditModel reviewEditModel, string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException();

                await _reviewService.EditAsync(reviewEditModel, id, userId);

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
        [HttpGet("{id}")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var reviewDeleteViewModel = await _reviewService.GetReviewDeleteViewModelByIdAsync(id);

            return View(reviewDeleteViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteReview(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException();

                await _reviewService.DeleteAsync(id, userId);

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