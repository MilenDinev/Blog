using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
