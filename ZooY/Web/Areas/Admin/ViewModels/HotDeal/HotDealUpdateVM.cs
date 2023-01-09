namespace Web.Areas.Admin.ViewModels.HotDeal
{
    public class HotDealUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
    }
}
