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
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardService dashboardRespository, IPhotoService photoService)
        {
            _dashboardService = dashboardRespository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userParties = await _dashboardService.GetAllUserParties();
            var userClubs = await _dashboardService.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Parties = userParties,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}
