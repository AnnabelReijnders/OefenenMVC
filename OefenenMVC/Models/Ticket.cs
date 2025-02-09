﻿using System.ComponentModel.DataAnnotations;

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

        public int Quantity { get; set; } 
        public decimal TotalPrice { get; set; } 

    }
}
