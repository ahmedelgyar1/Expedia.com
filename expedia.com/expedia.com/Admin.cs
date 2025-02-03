using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    internal class Admin
    {
        internal string AdminId { get; set; }
        internal string Name { get; set; }
        internal string Email { get; set; }
        internal string Password { get; set; }
        internal static List<HotelBooking>hotelBookings = new List<HotelBooking>();
        internal static List<FlightBooking>FlightBookings= new List<FlightBooking>();
        public Admin(string adminId, string name, string email, string password)
        {
            AdminId = adminId;
            Name = name;
            Email = email;
            Password = password;
        }

  
        internal void AddHotel(List<Hotel> hotels, Hotel newHotel)
        {
            hotels.Add(newHotel);
            Console.WriteLine($"Hotel '{newHotel.HotelName}' added successfully.");
        }

        internal void RemoveHotel(List<Hotel> hotels, string hotelName)
        {
            var hotel = hotels.FirstOrDefault(h => h.HotelName == hotelName);
            if (hotel != null)
            {
                hotels.Remove(hotel);
                Console.WriteLine($"Hotel '{hotelName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Hotel '{hotelName}' not found.");
            }
        }

        internal void ViewAllUsers(List<User> users)
        {
            Console.WriteLine("\nRegistered Users:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Name",-20} | {"Email",-30}");
            Console.WriteLine(new string('-', 50));

            foreach (var user in users)
            {
                Console.WriteLine($"{user.GetFullName(),-20} | {user.Email,-30}");
            }
            Console.WriteLine(new string('-', 50));
        }


        internal void GenerateRevenueReport()
        {
            double totalRevenue = hotelBookings.Sum(b => b.TotalPrice);
            Console.WriteLine("\nRevenue Report:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Total Revenue:",-20} ${totalRevenue}");
            Console.WriteLine(new string('-', 50));
        }
    }
}
