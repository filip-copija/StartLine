using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StartLine_social_network.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is reuqired.")]
        [DisplayName("Login")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is reuqired.")]
        [DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
