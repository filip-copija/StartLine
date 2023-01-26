using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;

namespace StartLine_social_network.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(AppUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0) return true;
            else return false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
