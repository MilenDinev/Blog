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
        private readonly IToolService _toolService;

        public HomeController(ILogger<HomeController> logger, IToolService toolService, UserManager<User> userManager)
        {
            _logger = logger;
            _toolService = toolService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var tools = await _toolService.GetToolPreviewModelBundleAsync();

            if (!string.IsNullOrEmpty(search))
            {
                tools = tools.Where(tool => tool.Title.Contains(search)).ToList();
                
            }

            return View(tools);
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