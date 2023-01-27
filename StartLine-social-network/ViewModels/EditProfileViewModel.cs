namespace StartLine_social_network.ViewModels
{
    public class EditProfileViewModel
    {
        public string? UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public IFormFile? Image { get; set; }
    }
}
