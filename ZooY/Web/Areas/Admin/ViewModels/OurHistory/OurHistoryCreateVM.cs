namespace Web.Areas.Admin.ViewModels.OurHistory
{
    public class OurHistoryCreateVM
    {
        public string Title { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
