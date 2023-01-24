using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;

namespace StartLine_social_network.Service
{
    public class PartyService : IPartyService
    {
        private readonly AppDbContext _context;
        public PartyService(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Party party)
        {
            _context.Add(party);
            return Save();
        }

        public bool Delete(Party party)
        {
            _context.Remove(party);
            return Save();
        }

        public async Task<IEnumerable<Party>> GetAllElements()
        {
            // returns a whole entire list
            return await _context.Parties.ToListAsync();
        }

        public async Task<Party> GetByIdAsync(int id)
        {
            // this one returns just one 
            return await _context.Parties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Party>> GetPartyCity(string city)
        {
            // Moves from appdb > club > address and serach by the city
            return await _context.Parties.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0) return true;
            else return false;
        }

        public bool Update(Party party)
        {
            _context.Update(party);
            return Save();
        }
    }
}
