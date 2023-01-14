namespace Web.ViewModels.Shop
{
    public class ShopIndexVM
    {
        public List<Core.Entities.ProductCategory> ProductCategories { get; set; }
        public List<Core.Entities.Brand> Brands { get; set; }
        public List<Core.Entities.Product> Products { get; set; }

        public string? Title { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 3;

        public int PageCount { get; set; }
    }
}
