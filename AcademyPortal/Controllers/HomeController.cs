using System.Web.Mvc;

namespace AcademyPortal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
         public ActionResult ContactUs()
         {
            return View();
          }
     }
}
