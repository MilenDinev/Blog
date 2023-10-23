namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;
    using Data.Entities;
    using Data.Models.RequestModels.Review;
    using Models;
    using Services.Interfaces;
    using Blog.Data.Models.ViewModels.Review;

    [Route("Reviews")]
    public class ReviewsController : Controller
    {
        IReviewService reviewService;
        UserManager<User> userManager;

        public ReviewsController(IReviewService reviewService, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.reviewService = reviewService;
        }

        [HttpGet("{id}")]
        [Route("Index")]
        public async Task<IActionResult> Index(string? id)
        {
            var reviewViewModel = await this.reviewService.GetReviewViewModelByIdAsync(id);
            // increace view//

            return View(reviewViewModel);
        }

        [HttpGet("{search}")]
        [Route("Latest")]
        public async Task<IActionResult> Latest(string? search)
        {
            var reviewsPreviewModelBundle = await this.reviewService.GetTodaysReviewPreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
            {
                reviewsPreviewModelBundle = reviewsPreviewModelBundle.Where(review => review.Title.Contains(search)).ToList();

            }

            return View(reviewsPreviewModelBundle);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ReviewCreateModel reviewCreateModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Create");
            }


            var reviewViewModel = new ReviewViewModel
            {
                Content = reviewCreateModel.Content,
                Description = reviewCreateModel.Description,
                Title = reviewCreateModel.Title,
                ImageUrl = reviewCreateModel.ImageUrl,
                ExternalArticleUrl = reviewCreateModel.ExternalArticleUrl,
                VideoUrl = reviewCreateModel.VideoUrl

            };

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.reviewService.CreateAsync(reviewCreateModel, userId);

            return View("Created", reviewViewModel);

        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reviewEditViewModel = await this.reviewService.GetReviewEditViewModelByIdAsync(id);

            if (reviewEditViewModel == null)
            {
                return NotFound();
            }

            return View(reviewEditViewModel);
        }

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
                var user = await this.userManager.GetUserAsync(this.User);
                string userId = user.Id;
                await this.reviewService.EditAsync(reviewEditModel, id, userId);

                return RedirectToAction("Index", "Home");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            var reviewEditViewModel = await this.reviewService.GetReviewEditViewModelByIdAsync(id);

            return View(reviewEditViewModel);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var reviewDeleteViewModel = await this.reviewService.GetReviewDeleteViewModelByIdAsync(id);

            return View(reviewDeleteViewModel);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteReview(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var isReviewExists = await this.reviewService.AnyByIdAsync(id);

            if (!isReviewExists)
            {
                return NotFound();
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string userId = user.Id;
                await this.reviewService.DeleteAsync(id, userId);

                return RedirectToAction("Index", "Home");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
