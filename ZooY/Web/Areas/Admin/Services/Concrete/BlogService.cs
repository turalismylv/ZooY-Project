using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Blog;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public BlogService(IBlogRepository blogRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _blogRepository = blogRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region HomeMainSliderCrud

        public async Task<BlogIndexVM> GetAllAsync()
        {
            var model = new BlogIndexVM
            {
                Blog = await _blogRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(BlogCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _blogRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Blog mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 1000))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 1000 kbdan boyukdur");
                return false;
            }

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photo", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.Photo, 1000))
            {
                _modelState.AddModelError("Photo", "File olcusu 1000 kbdan boyukdur");
                return false;
            }


            var blog = new Blog
            {
                Id = model.Id,
                Title = model.Title,
                CreatedAt = DateTime.Now,
                MainPhotoName = await _fileService.UploadAsync(model.MainPhoto),
                PhotoName=await _fileService.UploadAsync(model.Photo),
                DescriptionOne=model.DescriptionOne,
                DescriptionTwo=model.DescriptionTwo,
                DescriptionThree=model.DescriptionThree,
                DetailTitle=model.DetailTitle,
                Paragraph = model.Paragraph,
                Text = model.Text,
                Time = model.Time,

            };

            await _blogRepository.CreateAsync(blog);
            return true;
        }


        public async Task<BlogUpdateVM> GetUpdateModelAsync(int id)
        {


            var blog = await _blogRepository.GetAsync(id);

            if (blog == null) return null;

            var model = new BlogUpdateVM
            {
                Id = blog.Id,
                Title = blog.Title,
                MainPhotoName = blog.MainPhotoName,
                DescriptionOne = blog.DescriptionOne,
                DescriptionThree = blog.DescriptionThree,
                DescriptionTwo = blog.DescriptionTwo,
                DetailTitle=blog.DetailTitle,
                PhotoName=blog.PhotoName,
                Paragraph = blog.Paragraph,
                Text=blog.Text,
                Time=blog.Time,
            };

            return model;

        }

        public async Task<BlogDetailsVM> GetDetailsModelAsync(int id)
        {


            var blog = await _blogRepository.GetAsync(id);

            if (blog == null) return null;

            var model = new BlogDetailsVM
            {
                Id = blog.Id,
                DescriptionOne = blog.DescriptionOne,
                MainPhotoName = blog.MainPhotoName,
                PhotoName = blog.PhotoName,
                DescriptionThree=blog.DescriptionThree,
                DescriptionTwo=blog.DescriptionTwo,
                DetailTitle = blog.DetailTitle,
                Paragraph = blog.Paragraph,
                Text = blog.Text,
                Time = blog.Time,
                Title=blog.Title,
            };

            return model;
        }


        public async Task<bool> UpdateAsync(BlogUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _blogRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda blog mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 1000))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 1000 kbdan boyukdur");
                    return false;
                }
            }

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 1000))
                {
                    _modelState.AddModelError("Photo", "File olcusu 1000 kbdan boyukdur");
                    return false;
                }
            }

            var blog = await _blogRepository.GetAsync(model.Id);




            if (blog != null)
            {
                blog.Id = model.Id;
                blog.Title = model.Title;
                blog.ModifiedAt = DateTime.Now;
                blog.DetailTitle = model.DetailTitle;
                blog.DescriptionOne = model.DescriptionOne;
                blog.DescriptionTwo = model.DescriptionTwo;
                blog.DescriptionThree = model.DescriptionThree;
                blog.Paragraph = model.Paragraph;
                blog.Text = model.Text;
                blog.Time = model.Time;


                if (model.MainPhoto != null)
                {
                    blog.MainPhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }


                if (model.Photo != null)
                {
                    blog.PhotoName = await _fileService.UploadAsync(model.Photo);
                }

                await _blogRepository.UpdateAsync(blog);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);
            if (blog != null)
            {
                _fileService.Delete(blog.PhotoName);
                _fileService.Delete(blog.MainPhotoName);




                await _blogRepository.DeleteAsync(blog);

                return true;

            }

            return false;
        }



        #endregion

    }
}
