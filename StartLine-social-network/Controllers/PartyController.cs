using Microsoft.AspNetCore.Mvc;

namespace StartLine_social_network.Controllers
{
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
