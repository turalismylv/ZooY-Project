namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile MainPhoto { get; set; }

    }
}
