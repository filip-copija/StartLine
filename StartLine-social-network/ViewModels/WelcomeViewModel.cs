namespace StartLine_social_network.ViewModels
{
    public class WelcomeViewModel
    {
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public IFormFile? Image { get; set; }
    }
}
