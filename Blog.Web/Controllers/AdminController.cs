using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
