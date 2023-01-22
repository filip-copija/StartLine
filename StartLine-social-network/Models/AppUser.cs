using Microsoft.AspNetCore.Identity;

namespace StartLine_social_network.Models
{
    public class AppUser : IdentityUser
    {
        public int Parties { get; set; }
        public Address? Address { get; set; }
    }
}
