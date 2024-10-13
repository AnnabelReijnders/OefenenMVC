using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OefenenMVC.Models
{
    public class Organizer
    {
        public int organizerID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public List<Event> events { get; set; }

        // Constructor
        public Organizer(int OrganizerID, string Name, string Email, string PhoneNumber)
        {
            organizerID = OrganizerID;
            name = Name;
            email = Email;
            phoneNumber = PhoneNumber;
            events = new List<Event>();
        }

        // Methods
        public void CreateEvent(Event newEvent)
        {
            events.Add(newEvent);
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            // Code to update event
        }

        public void DeleteEvent(Event eventToDelete)
        {
            events.Remove(eventToDelete);
        }

        public List<Event> ViewEventList()
        {
            return events;
        }
    }
}
