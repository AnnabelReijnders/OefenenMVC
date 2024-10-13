using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OefenenMVC.Db;
using OefenenMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OefenenMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly VoorbeeldDb _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(VoorbeeldDb context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Actie voor het weergeven van accountdetails
        public async Task<IActionResult> Details()
        {
            var userEmail = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Verwijder de bewerkfunctionaliteit

        // Actie voor het weergeven van aangekochte tickets
        public async Task<IActionResult> Tickets()
        {
            var userEmail = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var userTickets = await _context.Tickets
                .Include(t => t.Event)
                .Where(t => t.UserId == user.Id)
                .ToListAsync();

            return View(userTickets);
        }
    }
}
