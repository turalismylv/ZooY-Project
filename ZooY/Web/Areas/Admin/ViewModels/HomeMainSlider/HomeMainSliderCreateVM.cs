namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public IFormFile MainPhoto { get; set; }

        public string LearnUrl { get; set; }
    }
}
