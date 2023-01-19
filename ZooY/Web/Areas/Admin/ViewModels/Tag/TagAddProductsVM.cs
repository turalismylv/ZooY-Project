using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Tag
{
    public class TagAddProductsVM
    {
        public int TagId { get; set; }

        public List<int> ProductsIds { get; set; }

        public List<SelectListItem>? Products { get; set; }
    }
}
