using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
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
    }
}
