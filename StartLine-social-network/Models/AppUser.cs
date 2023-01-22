using Microsoft.AspNetCore.Identity;

namespace StartLine_social_network.Models
{
    public class AppUser : IdentityUser
    {
        public int PartyCounter { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set;}
        public ICollection<Party> Parties { get; set; }
    }
}
