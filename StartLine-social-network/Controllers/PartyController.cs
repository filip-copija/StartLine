using Microsoft.AspNetCore.Mvc;
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
    }
}
