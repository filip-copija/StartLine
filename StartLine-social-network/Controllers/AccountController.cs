using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Username);

            if (user != null)
            {
                // User exists, checking password here
                var passCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passCheck)
                {
                    // Password correcnt, signing in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Party"); // Action name, Controller name
                    }
                }
                // Password incorrect
                TempData["Error"] = "Login or password is incorrect. Please try again";
                // Try again
                return View(loginVM);
            }
            // Not found
            TempData["Error"] = "Login or password is incorrect. Please try again";
            return View(loginVM);
        }
    }
}
