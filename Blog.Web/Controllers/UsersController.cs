namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Interfaces;
    using System.Security.Claims;

    [Authorize]
    [Route("users")]
    public class UsersController : Controller
    {
       private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        private string CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                ?? throw new UnauthorizedAccessException();


        [HttpPost("favorites/add/{id}")]
        public async Task<IActionResult> AddFavorite(string id)
        {
            return await HandleFavoriteChange(id, true);
        }


        [HttpPost("favorites/remove/{id}")]
        public async Task<IActionResult> RemoveFavorite(string id)
        {
            return await HandleFavoriteChange(id, false);
        }


        [HttpGet("favorites")]
        public async Task<IActionResult> Favorites(string? search)
        {
            var favoriteReviewsModel = await _userService.GetFavoriteReviewsAsync(CurrentUserId);

            if (!string.IsNullOrEmpty(search))
            {
                favoriteReviewsModel = favoriteReviewsModel.Where(review => review.Title.Contains(search)).ToList();
            }

            return View(favoriteReviewsModel);
        }


        [HttpGet("vote/{reviewId}")]
        public async Task<IActionResult> Vote(string reviewId, bool isUpVote)
        {
            if (string.IsNullOrEmpty(reviewId))
            {
                return BadRequest();
            }

            var votesModel = await _userService.VoteAsync(isUpVote, reviewId, CurrentUserId);

            return Json(new { success = true, upVotes = votesModel.UpVotes, downVotes = votesModel.DownVotes });
        }


        [NonAction]
        private async Task<IActionResult> HandleFavoriteChange(string id, bool add)
        {
            try
            {
                if (add)
                {
                    await _userService.AddFavoriteReviewAsync(CurrentUserId, id);
                }
                else
                {
                    await _userService.RemoveFavoritesReviewAsync(CurrentUserId, id);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
