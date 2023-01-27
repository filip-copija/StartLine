using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StartLine_social_network.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileName { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Party>? Parties { get; set; }
        public string? Description { get; set; }
    }
}
