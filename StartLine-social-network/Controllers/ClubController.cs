using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Extensions;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;

namespace StartLine_social_network.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubService clubService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubService = clubService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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
            // GUID - A Globally Unique Identifier 
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = currentUserId };
            return View(createClubViewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        Street = clubVM.Address.Street,
                        Province = clubVM.Address.Province,
                    },
                    ClubCategory = clubVM.ClubCategory,
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
                URL = club.Image,
                AddressId = club.AddressId,
                Address = new Address
                {
                    City = club.Address.City,
                    Street = club.Address.Street,
                    Province = club.Address.Province,
                },
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", clubVM); // view, model
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
                    ModelState.AddModelError("", "There was a problem occurred while making this action");
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
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        Street = clubVM.Address.Street,
                        Province = clubVM.Address.Province,
                    },
                    ClubCategory = clubVM.ClubCategory,
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
        [Authorize]
        // Delete data
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubInfo = await _clubService.GetByIdAsync(id);
            if (clubInfo == null) return View("Error");

            _clubService.Delete(clubInfo);
            return RedirectToAction("Index");
        }
        public IActionResult Return()
        {
            return RedirectToAction("Index");
        }
    }
}