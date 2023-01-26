using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Extensions;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.ProfileName = editVM.ProfileName;
            user.ProfileImageUrl = photoResult.Url.ToString();
            Address address = new Address()
            {
                City = editVM.Address.City,
                Street = editVM.Address.Street,
                Province = editVM.Address.Province,
            };
        }

        public DashboardController(IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService)
        {
            _dashboardService = dashboardService;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var userParties = await _dashboardService.GetAllUserParties();
            var userClubs = await _dashboardService.GetAllUserClubs();
            var dashboardVM = new DashboardViewModel()
            {
                Parties = userParties,
                Clubs = userClubs,
            };
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardService.GetUsedById(currentUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                ProfileName = user.ProfileName,
                ProfileImageUrl = user.ProfileImageUrl,
                Address = new Address()
                {
                    City = user.Address.City,
                    Street = user.Address.Street,
                    Province = user.Address.Province,
                },
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashboardService.GetByIdNoTracking(editVM.Id);
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardService.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardService.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
