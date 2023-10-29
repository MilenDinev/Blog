namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore.Storage;
    using Data.Entities;
    using Data.Models.RequestModels.Review;
    using Services.Interfaces;

    [Route("Reviews")]
    public class ReviewsController : Controller
    {
        IReviewService _reviewService;
        IUserService _userService;
        UserManager<User> _userManager;

        public ReviewsController(IReviewService reviewService, IUserService userService, UserManager<User> userManager)
        {
            _reviewService = reviewService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(string? id)
        {
            var reviewViewModel = await _reviewService.GetReviewViewModelByIdAsync(id);
            // increace view//
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                reviewViewModel.IsFavorite = user.FavoriteReviews.Any(x => x.Id == id);
            }

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
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var user = await _userManager.GetUserAsync(User);
            string userId = user.Id;
            await _reviewService.CreateAsync(reviewCreateModel, userId);

            return RedirectToAction("Index", "Home");

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
                var user = await _userManager.GetUserAsync(User);
                string userId = user.Id;
                await _reviewService.EditAsync(reviewEditModel, id, userId);

                return RedirectToAction("Index", "Home");
            }
            catch (RetryLimitExceededException /* dex */)
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

            var isReviewExists = await _reviewService.AnyByIdAsync(id);

            if (!isReviewExists)
            {
                return NotFound();
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                string userId = user.Id;
                await _reviewService.DeleteAsync(id, userId);

               return RedirectToAction("Index", "Home");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [Route("Add-Favorite/{id}")]
        public async Task<IActionResult> AddFavorite(string id)
        {
            var userId = _userManager.GetUserId(User);

            try
            {
                await _userService.AddReviewToFavorite(userId, id);
                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                // Handle the exception as needed (e.g., return an error view)
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Remove-Favorite/{id}")]
        public async Task<IActionResult> RemoveFavorite(string id)
        {
            var userId = _userManager.GetUserId(User);

            try
            {
                await _userService.RemoveReviewFromFavorites(userId, id);
                return Json(new { success = true  });
            }
            catch (Exception ex)
            {
                // Handle the exception as needed (e.g., return an error view)
                return View("Error");
            }
        }

    }
}
