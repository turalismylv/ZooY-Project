namespace Web.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialCreateVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public IFormFile MainPhoto { get; set; }

    }
}
