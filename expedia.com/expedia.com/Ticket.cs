using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    internal class Ticket
    {
        public int TicketId { get; set; }
        public DateTime DepartureTime {  get; set; }

        public DateTime ArrivalTime {  get; set; }
        public double Price { get; set; }

        public bool IsBooked {  get; set; }

        internal void ticketDetails()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Ticket Details");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"{"Ticket ID:",-15} {TicketId}");
            Console.WriteLine($"{"Departure Time:",-15} {DepartureTime}");
            Console.WriteLine($"{"Arrival Time:",-15} {ArrivalTime}");
            Console.WriteLine($"{"Price:",-15} ${Price}");
            Console.WriteLine(new string('=', 50));
        }
        public void BookTicket()
        {
            if (!IsBooked)
            {
                
                IsBooked = true;
                Console.WriteLine($"Ticket {TicketId} is successfully booked.");
            }
            else
            {
                Console.WriteLine($"Ticket {TicketId} is already booked.");
            }
        }
    }
}
