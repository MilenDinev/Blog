namespace Blog.Web.Controllers
{
    using Data.Models.RequestModels.Article;
    using Services.Interfaces;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Blog.Data.Models.ResponseModels.Article;

    public class ArticlesController : Controller
    {
        IArticleService articleService;
        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Board()
        {

            var createdArticles = await this.articleService.GetAllAsync();

            var articleViewModels = new List<ArticleViewModel>();
            foreach (var article in createdArticles)
            {
                var articleViewModel = new ArticleViewModel
                {
                    Id = article.Id,
                    Content = article.Content,
                    Title = article.Title,
                    ImageUrl = article.ImageUrl,
                    VideoUrl = article.VideoUrl,
                    ExternalArticleUrl = article.ExternalArticleUrl,
                };

                articleViewModels.Add(articleViewModel);
            }

            return View(articleViewModels);
        }       
        
        [HttpGet]
        public async Task<IActionResult> Article(string articleId)
        {

            var articlelistModel = await this.articleService.GetByIdAsync(articleId);

            var articleViewModel = new ArticleViewModel
            {
                Content = articlelistModel.Content,
                Title = articlelistModel.Title,
                ImageUrl = articlelistModel.ImageUrl,   
                VideoUrl = articlelistModel.VideoUrl,
                ExternalArticleUrl = articlelistModel.ExternalArticleUrl,
            };

            return View(articleViewModel);
        }

        [HttpGet]
        public IActionResult Created()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateBoard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateModel articleCreateModel)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateBoard");
            }

            string userId = "11c7c7ac-bced-47d8-b619-70ef16524e10";
            await  this.articleService.CreateAsync(articleCreateModel, userId);

            var articleViewModel = new ArticleViewModel
            {
                Content = articleCreateModel.Content,
                Title = articleCreateModel.Title,
                ImageUrl= articleCreateModel.ImageUrl,
                ExternalArticleUrl = articleCreateModel.ExternalArticleUrl,
                VideoUrl = articleCreateModel.VideoUrl
                
            };
            return View(articleViewModel);
        }
    }
}
