using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [MinLength(6, ErrorMessage = "Het wachtwoord moet minimaal 6 tekens lang zijn.")]
        public string? Password { get; set; }


        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
