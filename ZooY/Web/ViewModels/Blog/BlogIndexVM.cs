namespace Web.ViewModels.Blog
{
    public class BlogIndexVM
    {
        public List<Core.Entities.Blog> Blogs { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 6;

        public int PageCount { get; set; }
    }
}
