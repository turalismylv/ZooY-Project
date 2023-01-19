using Core.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services.Abstract;
using Web.Areas.Admin.ViewModels.Tag;
using Web.Areas.Admin.Services.Abstract;
using DataAccess.Repositories.Abstract;

namespace Web.Areas.Admin.Services.Concrete
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly ModelStateDictionary _modelState;

        public TagService(ITagRepository tagRepository,
            IProductRepository productRepository,
            IProductTagRepository productTagRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _tagRepository = tagRepository;
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }



        public async Task<TagIndexVM> GetAllAsync()
        {
            var model = new TagIndexVM
            {
                Tags = await _tagRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(TagCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _tagRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Tag mövcuddur");
                return false;
            }

            var tag = new Tag
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };

            await _tagRepository.CreateAsync(tag);
            return true;
        }

        public async Task<TagUpdateVM> GetUpdateModelAsync(int id)
        {


            var tag = await _tagRepository.GetAsync(id);

            if (tag == null) return null;

            var model = new TagUpdateVM
            {
                Id = tag.Id,
                Title = tag.Title,
            };

            return model;

        }

        public async Task<bool> UpdateAsync(TagUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _tagRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda tag mövcuddur");
                return false;
            }
       

            var tag = await _tagRepository.GetAsync(model.Id);




            if (tag != null)
            {
                tag.Id = model.Id;
                tag.Title = model.Title;
                tag.ModifiedAt = DateTime.Now;

                await _tagRepository.UpdateAsync(tag);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag != null)
            {
                await _tagRepository.DeleteAsync(tag);
                return true;

            }

            return false;
        }

        public async Task<TagDetailsVM> GetDetailsModelAsync(int id)
        {
            var tag = await _tagRepository.GetWithProductsAsync(id);
            if (tag == null) return null;

            var model = new TagDetailsVM
            {
                Tag = tag
            };

            return model;
        }


        public async Task<bool> AddProductsAsync(TagAddProductsVM model)
        {
            if (!_modelState.IsValid) return false;

            var tag = await _tagRepository.GetAsync(model.TagId);
            if (tag == null)
            {
                _modelState.AddModelError("TagId", "Tag tapılmadı");
                return false;
            }

            foreach (var productId in model.ProductsIds)
            {
                var product = await _productRepository.GetAsync(productId);
                if (product == null)
                {
                    _modelState.AddModelError(string.Empty, $"{productId} id-li Product tapılmadı");
                    return false;
                }

                var isExist = await _productTagRepository.AnyAsync(ct => ct.ProductId == productId && ct.TagId == tag.Id);
                if (isExist)
                {
                    _modelState.AddModelError(string.Empty, $"{productId} id-li Product artıq bu taga əlavə olunub");
                    return false;
                }

                var productTag = new ProductTag
                {
                    TagId = tag.Id,
                    ProductId = product.Id
                };

                await _productTagRepository.CreateAsync(productTag);
            }

            return true;
        }

        public async Task<TagAddProductsVM> GetTagAddProductsModel(int id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag == null) return null;

            var products = await _productRepository.GetAllAsync();

            var model = new TagAddProductsVM
            {
                TagId = tag.Id,
                Products = products.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                })
                .ToList()
            };

            return model;
        }
    }
}
