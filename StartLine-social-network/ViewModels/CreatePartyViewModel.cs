using StartLine_social_network.Data.Enum;
using StartLine_social_network.Models;

namespace StartLine_social_network.ViewModels
{
    // Represents the data displayed on Create View. Used for uploading photos from file
    public class CreatePartyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public PartyClubCategory PartyClubCategory { get; set; }
    }
}
