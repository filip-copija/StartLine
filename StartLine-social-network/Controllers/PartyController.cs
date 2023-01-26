using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Extensions;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyService _partyService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartyController(IPartyService partyService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _partyService = partyService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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
            // GUID - A Globally Unique Identifier 
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createPartyViewModel = new CreatePartyViewModel { AppUserId = currentUserId };
            return View(createPartyViewModel);
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
                    AppUserId = partyVM.AppUserId,
                    Address = new Address
                    {
                        City = partyVM.Address.City,
                        Street = partyVM.Address.Street,                       
                        Province = partyVM.Address.Province,
                    },
                    EntryFee = partyVM.EntryFee,
                    Website = partyVM.Website,
                    Facebook = partyVM.Facebook,
                    Contact = partyVM.Contact,
                    PartyClubCategory = partyVM.PartyClubCategory,
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
        // Passing data to the model
        public async Task<IActionResult> Edit(int id)
        {
            var party = await _partyService.GetByIdAsync(id);
            if (party == null) return View("Error");

            var partyVM = new EditPartyViewModel
            {
                Title = party.Title,
                Description = party.Description,
                URL = party.Image,
                AddressId = party.AddressId,
                Address = new Address
                {
                    City = party.Address.City,
                    Street = party.Address.Street,
                    Province = party.Address.Province,
                },                
                EntryFee = party.EntryFee,
                Website = party.Website,
                Facebook = party.Facebook,
                Contact = party.Contact,
                PartyClubCategory = party.PartyClubCategory,
            };
            return View(partyVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel partyVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", partyVM);
            }
            var userParty = await _partyService.GetByIdAsyncNoTracking(id);

            if (userParty != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userParty.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the photo");
                    return View(partyVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(partyVM.Image);

                var party = new Party
                {
                    Id = id,
                    Title = partyVM.Title,
                    Description = partyVM.Description,
                    Image = photoResult.Url.ToString(),                 
                    AddressId = partyVM.AddressId,
                    Address = new Address
                    {
                        City = partyVM.Address.City,
                        Street = partyVM.Address.Street,
                        Province = partyVM.Address.Province,
                    },             
                };

                _partyService.Update(party);

                return RedirectToAction("Index");
            }
            else
            {
                return View(partyVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var partyInfo = await _partyService.GetByIdAsync(id);
            if (partyInfo == null) return View("Error");
            return View(partyInfo);
        }
        [HttpPost, ActionName("Delete")]
        // Delete data
        public async Task<IActionResult> DeleteParty(int id)
        {
            var partyInfo = await _partyService.GetByIdAsync(id);
            if (partyInfo == null) return View("Error");

            _partyService.Delete(partyInfo);
            return RedirectToAction("Index");
        }
        public IActionResult Return()
        {
            return RedirectToAction("Index");
        }
    }
}
