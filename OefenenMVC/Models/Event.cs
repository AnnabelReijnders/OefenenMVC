using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Datum is verplicht.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Tijd is verplicht.")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Locatie is verplicht.")]
        public string? Location { get; set; }

        public string? Street { get; set; }
        public string? HouseNumber { get; set; }

        [Required(ErrorMessage = "Kosten zijn verplicht.")]
        [Range(0, 9999999.99, ErrorMessage = "Kosten moeten tussen 0 en 9999999.99 liggen.")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Maximale deelnemers is verplicht.")]
        [Range(1, 9999999, ErrorMessage = "Maximale deelnemers moet groter zijn dan 0.")]
        public int MaxParticipants { get; set; }

        public int SoldTickets { get; set; }
        public int AvailableSpots => MaxParticipants - SoldTickets;

        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string FullAddress => $"{Street} {HouseNumber}, {Location}";

        // Nieuwe eigenschap voor evenementtype
        public string EventType { get; set; }

        // Aangepaste validatie voor datum en tijd
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var eventDateTime = Date.Add(Time);
            var now = DateTime.Now;

            if (eventDateTime <= now)
            {
                yield return new ValidationResult(
                    "Datum en tijd moeten in de toekomst liggen.",
                    new[] { nameof(Date), nameof(Time) });
            }
        }
    }
}
