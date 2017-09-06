using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iglid.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iglid.Models;

namespace iglid.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly GameContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(GameContext gameContext,UserManager<ApplicationUser> um)
        {
            _context = gameContext;
            _userManager = um;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (!user.IsAdmin)
                RedirectToAction("Index", "Home");
            var disputes = _context.Disputes;
            return View(disputes);
        }

        

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}