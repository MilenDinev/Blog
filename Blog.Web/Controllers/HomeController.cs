namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Services.Interfaces;
    using Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewService _reviewService;

        public HomeController(ILogger<HomeController> logger, IReviewService reviewService, UserManager<User> userManager)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var reviews = await _reviewService.GetReviewPreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
            {
                reviews = reviews.Where(review => review.Title.Contains(search)).ToList();
                
            }

            return View(reviews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Conditions()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Terms()
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