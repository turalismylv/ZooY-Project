using Web.Areas.Admin.ViewModels.Tag;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ITagService
    {
        Task<TagIndexVM> GetAllAsync();
        Task<bool> CreateAsync(TagCreateVM model);
        Task<TagUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(TagUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddProductsAsync(TagAddProductsVM model);
        Task<TagAddProductsVM> GetTagAddProductsModel(int id);
        Task<TagDetailsVM> GetDetailsModelAsync(int id);
    }
}
