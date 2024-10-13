using System.ComponentModel.DataAnnotations;

namespace OefenenMVC.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int Quantity { get; set; }  // Aantal gekochte tickets
        public decimal TotalPrice { get; set; }  // Totale prijs van de gekochte tickets

        [Required]
        public string OrderNumber { get; set; } // Uniek ordernummer voor de aankoop
    }
}
