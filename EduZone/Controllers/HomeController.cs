using System;
using System.Configuration;
using System.Web.Mvc;
using EduZone.Models;
using EduZone.Repositories;

namespace EduZone.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            if(login.Email == null || login.Password == null)
            {
                ViewBag.Message = "Email and Password are required.";
                return View(login);
            }

            return RedirectToAction("BlogView", "Blog");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            try
            {
                AuthRepositories authRepositories = new AuthRepositories();
                authRepositories.RegisterUser(register);
                ViewBag.Message = "User registered successfully.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while registering the user." + ex;
                // Log the exception here if needed
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "EduZone - About Us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

       
    }
}