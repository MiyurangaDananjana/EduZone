using EduZone.Models;
using EduZone.Repositories;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult AdminDashboard()
        {
            DashboardRepositories dashboardRepositories = new DashboardRepositories();
            DashboardModel model = new DashboardModel();
            model.UserCount = dashboardRepositories.GetTotalUserCount();
            model.BlogsCount = dashboardRepositories.GetTotalBlogs();
            return View(model);
        }

        public ActionResult Users()
        {
            DashboardRepositories dashboardRepositories = new DashboardRepositories();
            UserModel model = new UserModel();
            model.userModels = dashboardRepositories.GetAllUserDetails();
            return View(model);
        }

        public ActionResult Stories()
        {
            DashboardRepositories stories = new DashboardRepositories();    
            BlogModel blog = new BlogModel();
            blog.Blogs = stories.GetAllBlogPostsWithUsers();
            return View(blog);
        }


    }
}