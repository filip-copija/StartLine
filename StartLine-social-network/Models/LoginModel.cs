using System.ComponentModel.DataAnnotations;

namespace StartLine_social_network.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is reuqired.")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is reuqired.")]
        public string Password { get; set; }
    }
}
