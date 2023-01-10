namespace Web.Areas.Admin.ViewModels.Welcome
{
    public class WelcomeUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
    }
}
