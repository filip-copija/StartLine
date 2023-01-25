using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        private readonly IPhotoService _photoService;

        public ClubController(IClubService clubService, IPhotoService photoService)
        {
            _clubService = clubService;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                { 
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    { 
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        Province = clubVM.Address.Province,
                    }
                };
                _clubService.Add(club);
                return RedirectToAction("Index");
            }         
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }
        // Passing data to the model
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubService.GetByIdAsync(id);
            if (club == null) return View("Error");

            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", clubVM);
            }
            var userClub = await _clubService.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the photo");
                    return View(clubVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                };

                _clubService.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var clubInfo = await _clubService.GetByIdAsync(id);
            if (clubInfo == null) return View("Error");
            return View(clubInfo);
        }
        [HttpPost, ActionName("Delete")]
        // Delete data
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubInfo = await _clubService.GetByIdAsync(id);
            if (clubInfo == null) return View("Error");

            _clubService.Delete(clubInfo);
            return RedirectToAction("Index");
        }
    }
}
