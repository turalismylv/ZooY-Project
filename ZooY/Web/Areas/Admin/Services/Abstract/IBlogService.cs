using Web.Areas.Admin.ViewModels.Blog;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IBlogService
    {
        Task<BlogIndexVM> GetAllAsync();

        Task<bool> CreateAsync(BlogCreateVM model);

        Task<BlogUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(BlogUpdateVM model);

        Task<bool> DeleteAsync(int id);
        Task<BlogDetailsVM> GetDetailsModelAsync(int id);
    }
}
