using System.ComponentModel.DataAnnotations;

namespace StartLine_social_network.ViewModels
{
    public class LoginViewModel
    {      
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
