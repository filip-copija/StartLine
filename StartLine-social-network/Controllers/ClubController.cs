using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;

namespace StartLine_social_network.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }
        public async Task<IActionResult> Index()
        {
            // Methods inherited by services
            IEnumerable<Club> clubs = await _clubService.GetAllElements();
            return View(clubs);
        }
        // Detail page for single element
        public async Task<IActionResult> Detail(int id)
        {
            // We use Include() to make join between Address.cs
            Club club = await _clubService.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Club club)
        {
            if(!ModelState.IsValid)
            {
                return View(club);
            }
            _clubService.Add(club);
            return RedirectToAction("Index");
        }
    }
}
