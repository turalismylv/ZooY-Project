namespace Web.ViewModels.About
{
    public class AboutIndexVM
    {
        public Core.Entities.Welcome Welcome { get; set; }
        public Core.Entities.OurHistory OurHistory { get; set; }

        public List<Core.Entities.Testimonial> Testimonials { get; set; }
    }
}
