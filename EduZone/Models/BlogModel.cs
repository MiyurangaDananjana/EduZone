using System;
using System.Collections.Generic;

namespace EduZone.Models
{
    public class BlogModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImgName { get; set; }

        public int CreateBy { get; set; }

        public string CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public int UserRole { get; set; }

        public List<BlogModel> Blogs { get; set; }
    }
}