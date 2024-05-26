using EduZone.Models;
using EduZone.Repositories;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace EduZone.Controllers
{
    public class BlogController : Controller
    {

        public ActionResult Stories()
        {
            BlogModel model = new BlogModel();
            BlogRepositories blogRepositories = new BlogRepositories();

 

            model.Blogs = blogRepositories.GetAllBlogPosts();
            return View(model);
        }
        // GET: BlogView
        public ActionResult BlogView()
        {
            int logUserId = Convert.ToInt32(Session["UserId"]);
            int accRole = Convert.ToInt32(Session["Role"]);

            BlogModel model = new BlogModel();
            BlogRepositories blogRepositories = new BlogRepositories();

            if (accRole == 1)
            {
                model.Blogs = blogRepositories.GetAllUsersBlogPosts();
                return View(model);
            }

            model.Blogs = blogRepositories.GetUserBlogPosts(logUserId);
            return View(model);
        }

        public ActionResult PostBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostBlog(BlogModel blogModel, HttpPostedFileBase ImageUpload)
        {
            int loginUserId = Convert.ToInt32(Session["UserId"]);

            string imageName = null;

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                imageName = Path.GetFileName(ImageUpload.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/blog_img"), imageName);
                ImageUpload.SaveAs(path);
            }

            BlogModel blog = new BlogModel
            {
                Title = blogModel.Title,
                Description = blogModel.Description,
                CreateBy = loginUserId,
                CreateDate = DateTime.Now,
                Status = 1,
                ImgName = imageName ?? "default.png"

            };

            BlogRepositories blogRepositories = new BlogRepositories();
            blogRepositories.CreateNewBlogPost(blog);
            return RedirectToAction("BlogView");
        }

        public ActionResult DeleteBlog(int id)
        {
            BlogRepositories blogRepositories = new BlogRepositories();
            blogRepositories.DeleteBlog(id);
            return RedirectToAction("BlogView");
        }

        public ActionResult UpdateBlog(int id)
        {
            BlogRepositories blogRepository = new BlogRepositories();
            BlogModel blog = blogRepository.GetBlogById(id); 
            return View(blog);
        }

        [HttpPost]
        public ActionResult UpdateBlog(BlogModel blog, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                string imageName = null;

                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
     
                    imageName = Path.GetFileName(ImageUpload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/blog_img"), imageName);
                    ImageUpload.SaveAs(path);
                }

                BlogModel blogEdit = new BlogModel
                {
                    Title = blog.Title,
                    Description = blog.Description,
                    ImgName = imageName ?? "default.png"
                };
                BlogRepositories blogRepository = new BlogRepositories();
                blogRepository.UpdateBlog(blogEdit); 
                return RedirectToAction("Index"); 
            }
            return View(blog); 
        }
    }
}