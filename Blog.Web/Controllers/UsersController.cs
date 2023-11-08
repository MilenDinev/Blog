namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Data.Entities;
    using Services.Interfaces;
    using System.Security.Claims;

    [Route("Users")]
    public class UsersController : Controller
    {
       private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpPost]
        [Route("Add-Favorite/{id}")]
        public async Task<IActionResult> AddFavorite(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            try
            {
                await _userService.AddFavoriteReviewAsync(userId, id);
                return Json(new { success = true });
            }
            catch (UnauthorizedAccessException)
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            try
            {
                await _userService.RemoveFavoritesReviewAsync(userId, id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Handle the exception as needed (e.g., return an error view)
                return View("Error");
            }
        }


        [Authorize]
        [HttpGet("{search}")]
        [Route("Favorites")]
        public async Task<IActionResult> Favorites(string? search)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            var votesModel = await _userService.VoteAsync(true, id, userId);

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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            var votesModel = await _userService.VoteAsync(false, id, userId);

            // Return the updated vote counts in the response
            return Json(new { success = true, upVotes = votesModel.UpVotes, downVotes = votesModel.DownVotes });
        }
    }
}
