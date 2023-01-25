using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartLine_social_network.Models
{
    public class AppUser : IdentityUser
    {
        public int PartyCounter { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set;}
        public ICollection<Party> Parties { get; set; }
    }
}
