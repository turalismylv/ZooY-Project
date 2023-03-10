namespace Web.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile Photo { get; set; }
        public string Text { get; set; }
        public string DescriptionOne { get; set; }
        public string DescriptionTwo { get; set; }
        public string DescriptionThree { get; set; }
        public string DetailTitle { get; set; }
        public DateTime Time { get; set; }
        public string Paragraph { get; set; }
    }
}
