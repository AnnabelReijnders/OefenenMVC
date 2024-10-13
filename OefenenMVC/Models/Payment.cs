using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OefenenMVC.Models
{
    using System;

    public class Payment
    {
        public int paymentID { get; set; }
        public string orderNumber { get; set; } // Relationship to Ticket
        public DateTime paymentDate { get; set; }
        public decimal amount { get; set; }
        public string paymentMethod { get; set; }

        // Constructor
        public Payment(int PaymentID, string OrderNumber, DateTime PaymentDate, decimal Amount, string PaymentMethod)
        {
            paymentID = PaymentID;
            orderNumber = OrderNumber;
            paymentDate = PaymentDate;
            amount = Amount;
            paymentMethod = PaymentMethod;
        }

        // Methods
        public void ProcessPayment()
        {
            //code
        }

        public void ConfirmPayment()
        {
            //code
        }
    }

}
