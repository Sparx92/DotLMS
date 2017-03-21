using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotLms.Web.Areas.Backoffice.Models
{
    public class AuthViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Challange")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Must complete the challange.")]
        public bool BotChallange { get; set; }
    }
}