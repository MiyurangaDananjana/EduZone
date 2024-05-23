using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}