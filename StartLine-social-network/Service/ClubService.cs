using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;

namespace StartLine_social_network.Service
{
    public class ClubService : IClubService
    {
        private readonly AppDbContext _context;
        public ClubService(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAllElements()
        {        
            // returns a whole entire list
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            // this one returns just one 
            return await _context.Clubs.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            // Moves from appdb > club > address and serach by the city
            return await _context.Clubs.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0) return true;
            else return false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
