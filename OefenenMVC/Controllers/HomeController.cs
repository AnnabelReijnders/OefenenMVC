using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OefenenMVC.Models;
using OefenenMVC.Db;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;

namespace OefenenMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VoorbeeldDb _context;

        public HomeController(ILogger<HomeController> logger, VoorbeeldDb context)
        {
            _logger = logger;
            _context = context;
        }

        // Haal evenementen op voor de Start pagina
        public async Task<IActionResult> Start()
        {
            try
            {
                var events = await _context.Events.ToListAsync(); // Haal evenementen op uit de database
                return View(events); // Geef de evenementen door aan de view
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het ophalen van evenementen.");
                return RedirectToAction("Error"); // Redirect naar een foutpagina
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    {
                        // Login is succesvol, maak een cookie aan en stuur door naar de Start pagina
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email)
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Start");
                    }
                    ModelState.AddModelError(string.Empty, "Ongeldig e-mailadres of wachtwoord.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fout bij login poging.");
                    ModelState.AddModelError(string.Empty, "Er is een fout opgetreden. Probeer het opnieuw.");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
                    if (userExists)
                    {
                        ModelState.AddModelError(string.Empty, "Dit e-mailadres is al in gebruik.");
                        return View(model);
                    }

                    var user = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fout bij registratie.");
                    ModelState.AddModelError(string.Empty, "Er is een fout opgetreden. Probeer het opnieuw.");
                }
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user != null)
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Login");
                    }
                    ModelState.AddModelError(string.Empty, "E-mailadres niet gevonden.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fout bij het resetten van wachtwoord.");
                    ModelState.AddModelError(string.Empty, "Er is een fout opgetreden. Probeer het opnieuw.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
