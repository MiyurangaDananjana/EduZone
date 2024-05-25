using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EduZone.Models;
using EduZone.Repositories;

namespace EduZone.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            AuthRepositories authRepositories = new AuthRepositories();
            bool isConnection = authRepositories.CheckDbConnection();
            if (!isConnection)
            {
                ViewBag.Message = "Connection Fail";
            }   
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            if (login.Email == null || login.Password == null)
            {
                ViewBag.Message = "Email and Password are required.";
                return View(login);
            }
            AuthRepositories authRepositories = new AuthRepositories(); 

            RegisterModel register =  authRepositories.GetUserByEmailAndPassword(login.Email, login.Password);
         
            if (register == null)
            {
                ViewBag.Message = "Email or Password is Incorrect.";
                return View(login);
            }
            if (register.UserId  > 0)
            {
                Session["UserId"] = register.UserId;
                Session["firstName"] = register.FirstName;
                Session["lastName"] = register.LastName;
                Session["Profile"] = register.ProfilePath;
                Session["Role"] = register.AccRole;
            }

            if(register.AccRole == 1)
            {
                return RedirectToAction("AdminDashboard", "Dashboard");
            }

            return RedirectToAction("BlogView", "Blog");

        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register, HttpPostedFileBase ImageUpload)
        {
            try
            {

                string imageName = null;

                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    imageName = Path.GetFileName(ImageUpload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/user_profile"), imageName);
                    ImageUpload.SaveAs(path);
                }

                RegisterModel model = new RegisterModel 
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Password    = register.Password,
                    Email = register.Email,
                    AccRole = 0,
                    ProfilePath = imageName ?? "default.png"

                };
                AuthRepositories authRepositories = new AuthRepositories();
                authRepositories.RegisterUser(model);
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

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUser(int Id)
        {
            AuthRepositories authRepositories = new AuthRepositories();
            authRepositories.DeleteUser(Id);
            return RedirectToAction("Users", "Dashboard");
        }


        public ActionResult UpdateUser( int Id)
        {
            AuthRepositories authRepo = new AuthRepositories();
            RegisterModel user = authRepo.GetUserById(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(RegisterModel model, HttpPostedFileBase ImageUpload)
        {
            if (model.UserId > 0)
            {
                AuthRepositories authRepo = new AuthRepositories();

                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageUpload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/blog_img"), fileName);
                    ImageUpload.SaveAs(path);
                    model.ProfilePath = fileName;
                }

                bool isUpdated = authRepo.UpdateUser(model);
                if (isUpdated)
                {
                    ViewBag.Message = "User updated successfully!";
                }
                else
                {
                    ViewBag.ErrorMessage = "Error updating user.";
                }
            }
            return View(model);
        }

    }
}