using Core.Entities;

namespace Web.ViewModels.Shop
{
    public class ShopDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PhotoName { get; set; }
        public double Price { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
