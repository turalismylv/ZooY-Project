namespace Web.Areas.Admin.ViewModels.Welcome
{
    public class WelcomeCreateVM
    {
        public string Title { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
    }
}
