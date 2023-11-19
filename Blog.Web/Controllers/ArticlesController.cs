namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore.Storage;
    using Data.Models.RequestModels.Article;
    using Services.Interfaces;
    using System.Security.Claims;

    [Route("articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        private string? CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var articlePreviewModelBundle = await _articleService.GetArticlePreviewModelBundleAsync();
            return View(articlePreviewModelBundle);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ArticleCreateModel articleCreateModel)
        {
            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (!ModelState.IsValid)
                return View();

            await _articleService.CreateAsync(articleCreateModel, CurrentUserId);

            return RedirectToAction(nameof(Dashboard));

        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return BadRequest();

            var articleEditViewModel = await _articleService.GetArticleEditViewModelByIdAsync(id);

            if (articleEditViewModel == null)
                return NotFound();

            return View(articleEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(ArticleEditModel articleEditModel, string? id)
        {

            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (id == null)
                return BadRequest();

            var articleEditViewModel = await _articleService.GetArticleEditViewModelByIdAsync(id);

            try
            {
                await _articleService.EditAsync(articleEditModel, id, CurrentUserId);

                return RedirectToAction(nameof(Dashboard));
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(articleEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
                return BadRequest();

            var articleDeleteViewModel = await _articleService.GetArticleDeleteViewModelByIdAsync(id);

            return View(articleDeleteViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteArticle(string? id)
        {
            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (id == null)
                return BadRequest();

            try
            {
                await _articleService.DeleteAsync(id, CurrentUserId);

                return RedirectToAction(nameof(Dashboard));
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
