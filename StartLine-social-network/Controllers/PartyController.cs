using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyService _partyService;
        private readonly IPhotoService _photoService;

        public PartyController(IPartyService partyService, IPhotoService photoService)
        {
            _partyService = partyService;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreatePartyViewModel partyVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(partyVM.Image);

                var party = new Party
                {
                    Title = partyVM.Title,
                    Description = partyVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = partyVM.Address.Street,
                        City = partyVM.Address.City,
                        Province = partyVM.Address.Province,
                    }
                };
                _partyService.Add(party);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(partyVM);
        }
    }
}
