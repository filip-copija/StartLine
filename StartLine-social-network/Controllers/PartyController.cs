using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;

namespace StartLine_social_network.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyService _partyService;
        public PartyController(IPartyService partyService)
        {
            _partyService = partyService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Party> party = await _partyService.GetAllElements();
            return View(party);
        }
        // Detail page for single element
        public async Task<IActionResult> Detail(int id)
        {
            // We use Include() to make join between Address.cs
            Party party = await _partyService.GetByIdAsync(id);
            return View(party);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Party party)
        {
            if (!ModelState.IsValid)
            {
                return View(party);
            }
            _partyService.Add(party);
            return RedirectToAction("Index");
        }
    }
}
