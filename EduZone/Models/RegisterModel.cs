using System;
using System.ComponentModel.DataAnnotations;

namespace EduZone.Models
{
    public class RegisterModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        public int AccRole { get; set; }

        public string ProfilePath { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-entering the password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RePassword { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}