using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static expedia.com.functionHelper;
namespace expedia.com
{
    
    
    internal class Flight
    {
        internal int FlightId { get; set; }
        internal string Airline { get; set; }        
        internal string DepartureLocation { get; set; }
        internal string ArrivalLocation { get; set; }
        internal DateTime DepartureTime { get; set; }
        internal DateTime ArrivalTime { get; set; }
        internal string seatClass {  get; set; }  

        internal List<Ticket> TicketList {  get; set; }= new List<Ticket>();
       
        internal void AddTickets(Ticket ticket)
        {
            TicketList.Add(ticket);
        }
        internal void GetFlightDetails()
        {
            Console.WriteLine("\nFlight Details:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Flight ID:",-15} {FlightId}");
            Console.WriteLine($"{"Airline:",-15} {Airline}");
            Console.WriteLine($"{"Departure:",-15} {DepartureLocation} at {DepartureTime}");
            Console.WriteLine($"{"Arrival:",-15} {ArrivalLocation} at {ArrivalTime}");
            Console.WriteLine($"{"Seat Class:",-15} {seatClass}");
            Console.WriteLine(new string('-', 50));
        }
    }

}
