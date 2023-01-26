using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IUserService userRepository, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _userService = userRepository;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in result)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Address = user.Address,
                    ProfileImageUrl = user.ProfileImageUrl ?? "/resources/avatar-male.jpg",
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Address = user.Address,
                ProfileImageUrl = user.ProfileImageUrl ?? "/resources/avatar-male.jpg",
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editVM = new EditUserDashboardViewModel()
            {
                Id = user.Id,
                ProfileName = user.ProfileName,
                ProfileImageUrl = user.ProfileImageUrl,
                Address = user.Address,
            };
            return View(editVM);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                editVM.ProfileImageUrl = user.ProfileImageUrl;

                await _userManager.UpdateAsync(user);

                return View(editVM);
            }

            user.Address.City = editVM.Address.City;
            user.Address.Street = editVM.Address.Street;
            user.Address.Province = editVM.Address.Province;
            user.ProfileName = editVM.ProfileName;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
    }
}
