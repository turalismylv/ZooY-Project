using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductAddTagsVM
    {
        public int ProductId { get; set; }
        public List<int> TagsIds { get; set; }
        public List<SelectListItem>? Tags { get; set; }
    }
}
