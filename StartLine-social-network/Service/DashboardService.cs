using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Extensions;
using StartLine_social_network.Models;

namespace StartLine_social_network.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(x => x.AppUser.Id == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Party>> GetAllUserParties()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userParties = _context.Parties.Where(x => x.AppUser.Id == currentUser);
            return userParties.ToList();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0) return true;
            else return false;
        }
    }
}
