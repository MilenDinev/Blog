namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Data.Entities;
    using Services.Interfaces;

    [Route("Users")]
    public class UsersController : Controller
    {
        IUserService _userService;
        UserManager<User> _userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("{search}")]
        [Route("Favorites")]
        public async Task<IActionResult> Favorites(string? search)
        {
            var userId = _userManager.GetUserId(User);

            var favoriteReviewsModel = await _userService.GetFavoriteReviewsAsync(userId);

            if (!string.IsNullOrEmpty(search))
            {
                favoriteReviewsModel = favoriteReviewsModel.Where(review => review.Title.Contains(search)).ToList();
            }

            return View(favoriteReviewsModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        [Route("UpVote/{id}")]
        public async Task<IActionResult> UpVote(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);

            var votesModel = await _userService.VoteAsync(true, id, user.Id);

            // Return the updated vote counts in the response
            return Json(new { success = true, upVotes = votesModel.UpVotes, downVotes = votesModel.DownVotes });
        }

        [Authorize]
        [HttpGet("{id}")]
        [Route("DownVote/{id}")]
        public async Task<IActionResult> DownVote(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var votesModel = await _userService.VoteAsync(false, id, user.Id);

            // Return the updated vote counts in the response
            return Json(new { success = true, upVotes = votesModel.UpVotes, downVotes = votesModel.DownVotes });
        }
    }
}
