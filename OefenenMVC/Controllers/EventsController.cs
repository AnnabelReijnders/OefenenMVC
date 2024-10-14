using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OefenenMVC.Db;
using OefenenMVC.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OefenenMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly VoorbeeldDb _context;

        public EventsController(VoorbeeldDb context)
        {
            _context = context;
        }

        // GET: Events
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var evenement = await _context.Events.FirstOrDefaultAsync(e => e.EventID == id);
            if (evenement == null)
            {
                return NotFound();
            }
            return View(evenement);
        }

        // GET: Events/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EventID,Name,Description,Date,Time,Location,Street,HouseNumber,Cost,MaxParticipants,Latitude,Longitude,EventType")] Event @event, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        @event.ImageData = memoryStream.ToArray();
                        @event.ImageMimeType = imageFile.ContentType;
                    }
                }

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var evenement = await _context.Events.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }
            return View(evenement);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Name,Description,Date,Time,Location,Street,HouseNumber,Cost,MaxParticipants,Latitude,Longitude,EventType")] Event @event, IFormFile? imageFile)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEvent = await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.EventID == id);
                    if (existingEvent == null)
                    {
                        return NotFound();
                    }

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            @event.ImageData = memoryStream.ToArray();
                            @event.ImageMimeType = imageFile.ContentType;
                        }
                    }
                    else
                    {
                        @event.ImageData = existingEvent.ImageData;
                        @event.ImageMimeType = existingEvent.ImageMimeType;
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/FilteredEvents
        public async Task<IActionResult> FilteredEvents(string eventType)
        {
            var events = await _context.Events
                .Where(e => e.EventType == eventType)
                .ToListAsync();

            return View(events);
        }

        // POST: Events/PurchaseTickets
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PurchaseTickets(int eventId, int quantity)
        {
            var eventDetails = await _context.Events.FindAsync(eventId);
            if (eventDetails == null)
            {
                return NotFound();
            }

            if (eventDetails.AvailableSpots < quantity) 
            {
                ModelState.AddModelError("", "Niet genoeg beschikbare plaatsen.");
                return RedirectToAction("Details", new { id = eventId });
            }

            var totalPrice = eventDetails.Cost * quantity;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var ticket = new Ticket
            {
                EventId = eventId,
                UserId = user.Id,
                Quantity = quantity,
                TotalPrice = totalPrice,
            };

            _context.Tickets.Add(ticket);
            eventDetails.SoldTickets += quantity;
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation");
        }

        // GET: Events/GetImage/5
        public IActionResult GetImage(int id)
        {
            var evenement = _context.Events.Find(id);
            if (evenement?.ImageData != null)
            {
                return File(evenement.ImageData, evenement.ImageMimeType);
            }
            return NotFound();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }


        public IActionResult OrderConfirmation()
        {
            return View(); 
        }
    }
}
