using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public IFormFile MainPhoto { get; set; }

        public int BrandId { get; set; }
        public List<SelectListItem>? Brands { get; set; }
    }
}
