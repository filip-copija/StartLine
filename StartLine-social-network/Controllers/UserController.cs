using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public  UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    UserName = user.UserName,
                    AddressId = user.AddressId,
                    Address = new Address
                    {
                        City = user.Address.City,
                        Street = user.Address.Street,
                        Province = user.Address.Province,
                    },
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userService.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                UserName = user.UserName,
                AddressId = user.AddressId,
                Address = new Address
                {
                    City = user.Address.City,
                    Street = user.Address.Street,
                    Province = user.Address.Province,
                },
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userDetailViewModel);
        }
    }
}
