namespace Blog.Web.Controllers
{
    using Services.Interfaces;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.Models.RequestModels.Tag;

    [Route("tags")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(TagCreateModel tagCreateModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            if (ModelState.IsValid)
            {
                await _tagService.CreateAsync(tagCreateModel, userId);

                return RedirectToAction("Dashboard", "Tools");
            }

            return View(tagCreateModel);
        }
    }
}
