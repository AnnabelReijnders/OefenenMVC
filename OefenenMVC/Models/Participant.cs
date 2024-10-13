using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OefenenMVC.Models
{
    public class Participant
    {
        public int participantID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        // Constructor
        public Participant(int ParticipantID, string Name, string Email, string PhoneNumber)
        {
            participantID = ParticipantID;
            name = Name;
            email = Email;
            phoneNumber = PhoneNumber;
        }

        // Methods
        public List<Event> ViewEventCalendar(List<Event> events)
        {
            return events;
        }

        public void ReserveTicket(Ticket ticket)
        {
            // Code to reserve a ticket
        }

        public void CancelReservation(Ticket ticket)
        {
            // Code to cancel reservation
        }
    }
}
