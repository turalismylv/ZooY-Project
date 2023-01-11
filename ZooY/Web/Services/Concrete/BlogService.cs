

using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Blog;

namespace Web.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _blogRepository = blogRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
  
        }


        public async Task<BlogIndexVM> GetAllAsync()
        {
            var model = new BlogIndexVM
            {
                Blogs = await _blogRepository.GetAllAsync()
            };
            return model;

        }



        public async Task<BlogDetailsVM> GetAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);

            if (blog == null) return null;

            var model = new BlogDetailsVM
            {
                Id = blog.Id,
                DescriptionOne = blog.DescriptionOne,
                MainPhotoName = blog.MainPhotoName,
                PhotoName = blog.PhotoName,
                DescriptionThree = blog.DescriptionThree,
                DescriptionTwo = blog.DescriptionTwo,
                DetailTitle = blog.DetailTitle,
                Paragraph = blog.Paragraph,
                Text = blog.Text,
                Time = blog.Time,
                Title = blog.Title,
            };
            return model;

        }
    }
}
