namespace Web.Areas.Admin.ViewModels.ProductCategory
{
    public class ProductCategoryCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile MainPhoto { get; set; }
    }
}
