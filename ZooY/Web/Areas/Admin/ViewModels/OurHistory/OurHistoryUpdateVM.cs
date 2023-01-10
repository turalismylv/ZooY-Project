namespace Web.Areas.Admin.ViewModels.OurHistory
{
    public class OurHistoryUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
