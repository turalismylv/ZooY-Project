namespace Web.ViewModels.Shop
{
    public class ShopProductIndexVM
    {
        public Core.Entities.ProductCategory ProductCategory { get; set; }
        public List<Core.Entities.Product> Products { get; set; }
    }
}
