namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Models.ResponseModels.Article;
    using Services.Interfaces;
    using Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, UserManager<User> userManager)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var articles = await _articleService.GetArticlePreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
            {
                articles = articles.Where(article => article.Title.Contains(search)).ToList();
                
            }

            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Conditions()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}