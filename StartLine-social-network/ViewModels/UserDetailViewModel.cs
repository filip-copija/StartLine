using StartLine_social_network.Models;

namespace StartLine_social_network.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
