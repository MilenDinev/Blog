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


        [HttpPost("favorites/add/{toolId}")]
        public async Task<IActionResult> AddFavorite(string toolId)
        {
            return await HandleFavoriteChange(toolId, true);
        }


        [HttpPost("favorites/remove/{toolId}")]
        public async Task<IActionResult> RemoveFavorite(string toolId)
        {
            return await HandleFavoriteChange(toolId, false);
        }


        [HttpGet("favorites")]
        public async Task<IActionResult> Favorites(string? search)
        {
            var favoriteToolsModel = await _userService.GetFavoriteToolsAsync(CurrentUserId);

            if (!string.IsNullOrEmpty(search))
            {
                favoriteToolsModel = favoriteToolsModel.Where(tool => tool.Title.Contains(search)).ToList();
            }

            return View(favoriteToolsModel);
        }


        [HttpGet("vote/{toolId}")]
        public async Task<IActionResult> Vote(string toolId, bool isUpVote)
        {
            if (string.IsNullOrEmpty(toolId))
            {
                return BadRequest();
            }

            var votesModel = await _userService.VoteAsync(isUpVote, toolId, CurrentUserId);

            return Json(new { success = true, upVotes = votesModel.UpVotes, downVotes = votesModel.DownVotes });
        }


        [NonAction]
        private async Task<IActionResult> HandleFavoriteChange(string toolId, bool add)
        {
            try
            {
                if (add)
                {
                    await _userService.AddFavoriteToolAsync(CurrentUserId, toolId);
                }
                else
                {
                    await _userService.RemoveFavoritesToolAsync(CurrentUserId, toolId);
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
