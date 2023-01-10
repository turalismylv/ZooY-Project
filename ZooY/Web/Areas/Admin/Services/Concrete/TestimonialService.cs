using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Testimonial;

namespace Web.Areas.Admin.Services.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public TestimonialService(ITestimonialRepository testimonialRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _testimonialRepository = testimonialRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        #region Testimonial

        public async Task<TestimonialIndexVM> GetAllAsync()
        {
            var model = new TestimonialIndexVM
            {
                Testimonials = await _testimonialRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<bool> CreateAsync(TestimonialCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _testimonialRepository.AnyAsync(c => c.FullName.Trim().ToLower() == model.FullName.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda  mövcuddur");
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



            var testiMonial = new Testimonial
            {
                Id = model.Id,
                FullName = model.FullName,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
            };

            await _testimonialRepository.CreateAsync(testiMonial);
            return true;
        }


        public async Task<TestimonialUpdateVM> GetUpdateModelAsync(int id)
        {


            var testimonial = await _testimonialRepository.GetAsync(id);

            if (testimonial == null) return null;

            var model = new TestimonialUpdateVM
            {
                Id = testimonial.Id,
                FullName = testimonial.FullName,
                Description = testimonial.Description,
                MainPhotoName = testimonial.PhotoName,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(TestimonialUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _testimonialRepository.AnyAsync(c => c.FullName.Trim().ToLower() == model.FullName.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda  mövcuddur");
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

            var testimonial = await _testimonialRepository.GetAsync(model.Id);




            if (testimonial != null)
            {
                testimonial.Id = model.Id;
                testimonial.FullName = model.FullName;
                testimonial.ModifiedAt = DateTime.Now;
                testimonial.Description = model.Description;


                if (model.MainPhoto != null)
                {
                    testimonial.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _testimonialRepository.UpdateAsync(testimonial);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetAsync(id);
            if (testimonial != null)
            {
                _fileService.Delete(testimonial.PhotoName);




                await _testimonialRepository.DeleteAsync(testimonial);

                return true;

            }

            return false;
        }



        #endregion

    }
}
