using StartLine_social_network.Data.Enum;
using StartLine_social_network.Models;

namespace StartLine_social_network.ViewModels
{
    // Used to edit and update data in Edit View
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
