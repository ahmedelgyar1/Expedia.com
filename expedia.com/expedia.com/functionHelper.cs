using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    public static class functionHelper
    {
      internal static Dictionary<string,User> users = new Dictionary<string,User>();
        internal static List<Hotel> hotelList= new List<Hotel>();
      internal static List<Flight> flightList = new List<Flight>();

        public enum TripType
        {
            OneWay,
            RoundTrip
        }

        internal static string verification()
       {
            Random random = new Random();
            string code =random.Next().ToString();
            return code; 

       }
        internal static DateTime GetCheckInDate()
        {
            Console.WriteLine("Enter the date you want go from (yyyy/MM/dd): ");
            string checkInDateInput = Console.ReadLine();
            DateTime checkInDate;
            while (!DateTime.TryParse(checkInDateInput, out checkInDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date.");
                checkInDateInput = Console.ReadLine();
            }
            return checkInDate;
        }

        internal static DateTime GetCheckOutDate()
        {
            Console.WriteLine("Enter check-out date (yyyy/MM/dd): ");
            string checkOutDateInput = Console.ReadLine();
            DateTime checkOutDate;
            while (!DateTime.TryParse(checkOutDateInput, out checkOutDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date.");
                checkOutDateInput = Console.ReadLine();
            }
            return checkOutDate;
        }
        internal static string GetCountry()
        {
            Console.WriteLine("Where to?");
            return Console.ReadLine();
        }
        internal static string GetLocation()
        {
            Console.WriteLine("Please choose the city you would like to go to.");
            return Console.ReadLine();
        }
       internal static string LeavingFrom()
        {
            Console.WriteLine("Where do you want to leave from?");
            return Console.ReadLine(); 
        }
    }
}
