using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int AccRole { get; set; }

        public string Password { get; set; }

        public string ProfileImg { get; set; }

        public DateTime CreateDateTime { get; set; }

        public List<UserModel> userModels { get; set; }
    }

}