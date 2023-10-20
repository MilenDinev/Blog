namespace Blog.Web.Controllers
{
    using Data.Models.RequestModels.Article;
    using Services.Interfaces;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Blog.Data.Models.ResponseModels.Article;
    using Blog.Data.Entities;

    public class ArticlesController : Controller
    {
        IArticleService articleService;
        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public IActionResult CreateBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditBoard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeleteBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var createdArticles = await this.articleService.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                // Filter articles based on the search query.
                createdArticles = createdArticles.Where(createdArticles => createdArticles.Title.Contains(search)).ToList();
            }


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
                    Creator = article.Creator,
                    CreationDate = article.CreationDate,
                    LastModifiedOn = article.LastModifiedOn,
                    UpVotes = article.UpVotes,
                    DownVotes = article.DownVotes,
                };

                articleViewModels.Add(articleViewModel);
            }

            return View(articleViewModels);
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
                    Creator = article.Creator,
                    CreationDate = article.CreationDate,
                    LastModifiedOn = article.LastModifiedOn,
                    UpVotes = article.UpVotes,
                    DownVotes = article.DownVotes,
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
                Title = articlelistModel.Title,
                Description = articlelistModel.Description,
                Content = articlelistModel.Content,
                ImageUrl = articlelistModel.ImageUrl,
                VideoUrl = articlelistModel.VideoUrl,
                ExternalArticleUrl = articlelistModel.ExternalArticleUrl,
                Creator = articlelistModel.Creator,
                CreationDate = articlelistModel.CreationDate,
                LastModifiedOn = articlelistModel.LastModifiedOn,
                UpVotes = articlelistModel.UpVotes,
                DownVotes = articlelistModel.DownVotes,
            };

            return View(articleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Created()
        {
            var articleslistModel = await this.articleService.GetAllAsync();

            var articleViewModels = new List<ArticleViewModel>();
            foreach (var article in articleslistModel)
            {
                var articleViewModel = new ArticleViewModel
                {
                    Id = article.Id,
                    Content = article.Content,
                    Title = article.Title,
                    ImageUrl = article.ImageUrl,
                    VideoUrl = article.VideoUrl,
                    ExternalArticleUrl = article.ExternalArticleUrl,
                    Creator = article.Creator,
                    CreationDate = article.CreationDate,
                    LastModifiedOn = article.LastModifiedOn,
                    UpVotes = article.UpVotes,
                    DownVotes = article.DownVotes,
                };

                articleViewModels.Add(articleViewModel);
            }

            return View(articleViewModels);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateModel articleCreateModel)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateBoard");
            }

            string userId = "981a4301-17de-458e-a0c6-574d887dd863";
            await  this.articleService.CreateAsync(articleCreateModel, userId);

            var articleViewModel = new ArticleViewModel
            {
                Content = articleCreateModel.Content,
                Description = articleCreateModel.Description,
                Title = articleCreateModel.Title,
                ImageUrl= articleCreateModel.ImageUrl,
                ExternalArticleUrl = articleCreateModel.ExternalArticleUrl,
                VideoUrl = articleCreateModel.VideoUrl
                
            };
            return View(articleViewModel);
        }

        [HttpPut("{articleId}")]
        public async Task<IActionResult> Edit(ArticleEditModel articleEditModel)
        {

            return View();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string articleId)
        {

            return View();
        }

    }
}
