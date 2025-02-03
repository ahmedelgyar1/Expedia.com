using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static expedia.com.functionHelper;

namespace expedia.com
{
    internal class FlightBooking
    {
        internal static int BookingID = 0; 
        public IUser Passenger { get;  set; }
        public Ticket DepartingTicket { get;  set; }
        public Ticket ReturningTicket {  get;  set; } 
        public string seatClass { get;  set; }
        public bool IsPaid { get;  set; }

        public FlightBooking( IUser passenger, Ticket DepartingTicket, string seatClass, Ticket? ReturningTicket = null)
        {
            ++BookingID; 
            Passenger = passenger;
            this.DepartingTicket =DepartingTicket;
            if (ReturningTicket!=null)
            {
                this.ReturningTicket= ReturningTicket; 
            }
            IsPaid = false;
            this.seatClass = seatClass; 
            
        }

        internal bool ProcessPayment()
        {
           

            Console.WriteLine("Enter your credit card number:");
            string cardNumber = Console.ReadLine();
            while(cardNumber.Length<12)
            {
                Console.WriteLine("Invalid Card Number");
                cardNumber =Console.ReadLine();
            }
            Console.WriteLine("Enter the expiration date (MM/YY):");
            string expirationDate = Console.ReadLine();

            Console.WriteLine("Enter the CVV code:");
            string cvv = Console.ReadLine();
            Payment payment; 
            if (this.ReturningTicket==null)
            {
                 payment = new Payment(cardNumber, expirationDate, cvv, DepartingTicket.Price);
            }
            else { 
               payment = new Payment(cardNumber, expirationDate, cvv, DepartingTicket.Price+ReturningTicket.Price);
            }
            if (payment.ProcessPayment())
            {
                IsPaid=true;
                DepartingTicket.IsBooked=true;
                if (ReturningTicket!=null)
                {
                    ReturningTicket.IsBooked=true;
                }
                Console.WriteLine($"Flight Booking ID: {BookingID} confirmed for {Passenger.FirstName} {Passenger.LastName}.");
                return true;
            }
            else
            {
                Console.WriteLine("Payment failed. Please try again.");
                return false;
            }
        }
       
        public void CancelBooking()
        {
            if (IsPaid)
            {
                Console.WriteLine($"Booking {BookingID} has been cancelled. Refund initiated.");
                IsPaid = false;
            }
            else
            {
                Console.WriteLine($"Booking {BookingID} is not paid yet, so no refund needed.");
            }
        }
    }

}
