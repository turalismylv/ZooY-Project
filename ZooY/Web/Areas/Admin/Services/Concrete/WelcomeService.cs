using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Welcome;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WelcomeService : IWelcomeService
    {
        private readonly IWelcomeRepository _welcomeRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public WelcomeService(IWelcomeRepository welcomeRepository, IActionContextAccessor actionContextAccessor, IFileService fileService)
        {
            _welcomeRepository = welcomeRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<WelcomeIndexVM> GetAsync()
        {
            var model = new WelcomeIndexVM
            {
                Welcome = await _welcomeRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(WelcomeCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 1000))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }

            var welcome = new Welcome
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.MainPhoto),
                Description = model.Description,
                FullName=model.FullName,
                Text = model.Text,
            };

            await _welcomeRepository.CreateAsync(welcome);

            return true;
        }

        public async Task<WelcomeUpdateVM> GetUpdateModelAsync(int id)
        {
            var welcome = await _welcomeRepository.GetAsync(id);

            if (welcome == null) return null;

            var model = new WelcomeUpdateVM
            {
                Id = welcome.Id,
                Title = welcome.Title,
                MainPhotoPath = welcome.PhotoName,
                Description = welcome.Description,
                FullName = welcome.FullName,
                Text=welcome.Text,
            };

            return model;

        }


        public async Task<bool> UpdateAsync(WelcomeUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 1000))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var welcome = await _welcomeRepository.GetAsync(model.Id);

            if (welcome != null)
            {
                welcome.Title = model.Title;
                welcome.ModifiedAt = DateTime.Now;
                welcome.Description = model.Description;
                welcome.FullName = model.FullName;
                welcome.Text = model.Text;



                if (model.MainPhoto != null)
                {
                    welcome.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }

                await _welcomeRepository.UpdateAsync(welcome);

            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var welcome = await _welcomeRepository.GetAsync(id);

            if (welcome != null)
            {
                _fileService.Delete(welcome.PhotoName);
                await _welcomeRepository.DeleteAsync(welcome);
                return true;
            }
            return false;
        }

    }
}
