using Web.ViewModels.Blog;

namespace Web.Services.Abstract
{
    public interface IBlogService
    {
        Task<BlogIndexVM> GetAllAsync();
        Task<BlogDetailsVM> GetAsync(int id);
    }
}
