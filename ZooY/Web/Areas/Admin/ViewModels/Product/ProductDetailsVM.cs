namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public Core.Entities.Product Product { get; set; }
        public int ProductCategoryId { get; set; }
        public Core.Entities.ProductCategory ProductCategory { get; set; }
        public int BrandId { get; set; }
        public Core.Entities.Brand Brand { get; set; }
    }
}
