﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OefenenMVC.Db;
using OefenenMVC.Models;

namespace WebApiAddOn.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApiEventsController : ControllerBase
	{
		private readonly VoorbeeldDb _context;

		public ApiEventsController(VoorbeeldDb context)
		{
			_context = context;
		}

		/// <summary>
		/// Haal een lijst van evenementen op.
		/// </summary>
		/// <returns>Een lijst van evenementen.</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
		{
			return await _context.Events.ToListAsync();
		}

		/// <summary>
		/// Haal een specifiek evenement op basis van het ID.
		/// </summary>
		/// <param name="id">Het ID van het evenement.</param>
		/// <returns>Het evenement met het opgegeven ID.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Event>> GetEvent(int id)
		{
			var @event = await _context.Events.FindAsync(id);

			if (@event == null)
			{
				return NotFound();
			}

			return @event;
		}

		/// <summary>
		/// Bewerk een specifiek evenement op basis van het ID.
		/// </summary>
		/// <param name="id">Het ID van het evenement.</param>
		/// <param name="event">Het bijgewerkte evenement.</param>
		/// <returns>Een actie-resultaat.</returns>
		/// <remarks>
		/// Voorbeeld van een verzoek:
		///
		///     PUT /ApiEvents/28
		///     {
		///         "eventID": 28,
		///         "name": "Test Event",
		///         "description": "Dit is een test evenement.",
		///         "date": "2024-10-13",
		///         "time": "12:00:00", 
		///         "location": "Test Locatie",
		///         "street": "Hoofdstraat",
		///         "houseNumber": "1",
		///         "cost": 10.00,
		///         "maxParticipants": 100,
		///         "soldTickets": 0,
		///         "imageData": null,
		///         "imageMimeType": null,
		///         "latitude": "50.8792533",
		///         "longitude": "5.9836698",
		///         "tickets": [],
		///         "eventType": "Cultureel"
		///     }
		/// </remarks>
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEvent(int id, Event @event)
		{
			if (id != @event.EventID)
			{
				return BadRequest();
			}

			var existingEvent = await _context.Events.FindAsync(id);
			if (existingEvent == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (@event.ImageData == null || @event.ImageData.Length == 0)
			{
				@event.ImageData = existingEvent.ImageData;
				@event.ImageMimeType = existingEvent.ImageMimeType;
			}

			_context.Entry(existingEvent).CurrentValues.SetValues(@event);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EventExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		/// <summary>
		/// Maak een nieuw evenement aan.
		/// </summary>
		/// <param name="event">Het nieuwe evenement.</param>
		/// <returns>Het aangemaakte evenement.</returns>
		/// /// <remarks>
		/// Voorbeeld van een verzoek:
		///
		///     POST /ApiEvents
		///     {
		///         "name": "Test Eventt",
		///         "description": "Dit is een test evenement",
		///         "date": "2024-10-14T12:00:00",
		///         "time": "00:00:00",
		///         "location": "Test Locatie",
		///         "street": null,
		///         "houseNumber": null,
		///         "cost": 10,
		///         "maxParticipants": 1,
		///         "soldTickets": 0,
		///         "availableSpots": 1,
		///         "imageData": null,
		///         "imageMimeType": null,
		///         "latitude": "50.8792533",
		///         "longitude": "5.9836698",
		///         "tickets": [],
		///         "fullAddress": " , Test Locatie",
		///         "eventType": "Overig"
		///     }
		/// </remarks>
		[HttpPost]
		public async Task<ActionResult<Event>> PostEvent([FromBody] Event @event)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Events.Add(@event);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetEvent", new { id = @event.EventID }, @event);
		}

		/// <summary>
		/// Verwijder een specifiek evenement op basis van het ID.
		/// </summary>
		/// <param name="id">Het ID van het evenement.</param>
		/// <returns>Een actie-resultaat.</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			var @event = await _context.Events.FindAsync(id);
			if (@event == null)
			{
				return NotFound();
			}

			_context.Events.Remove(@event);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool EventExists(int id)
		{
			return _context.Events.Any(e => e.EventID == id);
		}
	}
}
