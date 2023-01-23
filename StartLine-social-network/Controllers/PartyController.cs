using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartLine_social_network.Data;
using StartLine_social_network.Models;

namespace StartLine_social_network.Controllers
{
    public class PartyController : Controller
    {
        private readonly AppDbContext _context;
        public PartyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Party> clubs = _context.Parties.ToList();
            return View(clubs);
        }
        // Detail page for single element
        public IActionResult Detail(int id)
        {
            // We use Include() to make join between Address.cs
            Party party = _context.Parties.Include(x => x.Address).FirstOrDefault(x => x.Id == id);
            return View(party);
        }
    }
}
