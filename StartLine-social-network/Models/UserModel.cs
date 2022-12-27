using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StartLine_social_network.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Day")]
        public int DateDay { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Month")]
        public int DateMonth { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Year")]
        public int DateYear { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("Country")]
        public string Country { get; set; }
    }
}
