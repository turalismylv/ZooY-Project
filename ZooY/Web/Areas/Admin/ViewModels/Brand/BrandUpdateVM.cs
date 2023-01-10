namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? MainPhotoName { get; set; }
        public IFormFile? MainPhoto { get; set; }
    }
}
