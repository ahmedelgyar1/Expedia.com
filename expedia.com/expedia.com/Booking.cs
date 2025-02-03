using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    public class BookingManager
    {
        internal static void AddHotelBooking(HotelBooking booking)
        {
            
            Admin.hotelBookings.Add(booking);
        }
        internal static void AddFlightBooking(FlightBooking booking)
        {
            Admin.FlightBookings.Add(booking); 
        }
    }
}
