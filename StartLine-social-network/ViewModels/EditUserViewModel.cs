using StartLine_social_network.Models;

namespace StartLine_social_network.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileImageUrl { get; set;}        
        public Address? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
