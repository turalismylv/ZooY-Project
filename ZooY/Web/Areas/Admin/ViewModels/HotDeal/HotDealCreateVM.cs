namespace Web.Areas.Admin.ViewModels.HotDeal
{
    public class HotDealCreateVM
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public IFormFile MainPhoto { get; set; }
    }
}


