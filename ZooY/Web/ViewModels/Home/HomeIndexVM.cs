namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<Core.Entities.HomeMainSlider> HomeMainSliders { get; set; }
        public List<Core.Entities.Brand> Brands { get; set; }
        public List<Core.Entities.ProductCategory> ProductCategories { get; set; }
        public List<Core.Entities.Blog> Blogs { get; set; }
        public List<Core.Entities.Product> Products { get; set; }
        public Core.Entities.Find Find { get; set; }
        public Core.Entities.HotDeal HotDeal { get; set; }

    }
}
