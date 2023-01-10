namespace Web.Areas.Admin.ViewModels.ProductCategory
{
    public class ProductCategoryUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? MainPhotoName { get; set; }
        public IFormFile? MainPhoto { get; set; }
    }
}
