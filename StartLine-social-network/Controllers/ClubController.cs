﻿using Microsoft.AspNetCore.Mvc;
using StartLine_social_network.Data;
using StartLine_social_network.Models;

namespace StartLine_social_network.Controllers
{
    public class ClubController : Controller
    {
        private readonly AppDbContext _context;
        public ClubController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }
    }
}
