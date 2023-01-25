namespace StartLine_social_network.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set;}
        public string? ProfileName { get; set;}
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Province { get; set; }
        public IFormFile Image { get; set; }
    }
}
