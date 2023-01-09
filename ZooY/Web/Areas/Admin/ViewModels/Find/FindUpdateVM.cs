namespace Web.Areas.Admin.ViewModels.Find
{
    public class FindUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
      
        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
    }
}
