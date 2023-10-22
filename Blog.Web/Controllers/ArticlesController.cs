namespace Blog.Web.Controllers
{
    using Data.Models.RequestModels.Article;
    using Services.Interfaces;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Blog.Data.Models.ResponseModels.Article;
    using Blog.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Net;


    public class ArticlesController : Controller
    {
        IArticleService articleService;
        UserManager<User> userManager;

        public ArticlesController(IArticleService articleService, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.articleService = articleService;
        }


        [HttpGet]
        public async Task<ActionResult> Article(string id)
        {

            var articlelistModel = await this.articleService.GetArticleCompleteModelByIdAsync(id);

            return View(articlelistModel);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(ArticleCreateModel articleCreateModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Create");
            }


            var articleViewModel = new ArticleViewModel
            {
                Content = articleCreateModel.Content,
                Description = articleCreateModel.Description,
                Title = articleCreateModel.Title,
                ImageUrl = articleCreateModel.ImageUrl,
                ExternalArticleUrl = articleCreateModel.ExternalArticleUrl,
                VideoUrl = articleCreateModel.VideoUrl

            };

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.articleService.CreateAsync(articleCreateModel, userId);

            return View("Created", articleViewModel);

        }


        [HttpPost]
        public async Task<ActionResult> Edit(ArticleEditModel articleEditModel, string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var article = await this.articleService.GetArticleCompleteModelByIdAsync(id);
            var articleViewModel = new ArticleViewModel
            {
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                VideoUrl = article.VideoUrl,
                ExternalArticleUrl = article.ExternalArticleUrl,
            };

            {
                try
                {
                    var user = await this.userManager.GetUserAsync(this.User);
                    string userId = user.Id;
                    await this.articleService.EditAsync(articleEditModel, id, userId);

                    return RedirectToAction("Index", "Home");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(articleViewModel);
        }

        public async Task<ActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var article = await this.articleService.GetArticleCompleteModelByIdAsync(id);


            if (article == null)
            {
                return NotFound();
            }
            var articleViewModel = new ArticleViewModel
            {
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                VideoUrl = article.VideoUrl,
                ExternalArticleUrl = article.ExternalArticleUrl,
            };

            return View(articleViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var article = await this.articleService.GetArticlePreviewModelByIdAsync(id);

            var articleViewModel = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                ImageUrl = article.ImageUrl,
            };

            return View(articleViewModel);

        }


        [ActionName("Delete")]
        [HttpPost("{id}")]
        public async Task<ActionResult> DeleteArticle(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }


            var isArticleExists = await this.articleService.AnyByIdAsync(id);

            if (isArticleExists)
            {
                return NotFound();
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string userId = user.Id;
                await this.articleService.DeleteAsync(id, userId);

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
