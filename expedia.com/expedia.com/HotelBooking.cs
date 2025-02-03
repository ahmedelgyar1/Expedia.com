using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    internal class HotelBooking 
    {
        internal Hotel Hotel { get; set; }
        internal IUser User { get; set; }

        internal static int BookingId=1;
        internal DateTime CheckInDate { get; set; }
        internal DateTime CheckOutDate { get; set; }
        internal int NumberOfRooms { get; set; }
        internal double TotalPrice { get; set; }

       
        internal HotelBooking( Hotel hotel, IUser user, DateTime checkInDate, DateTime checkOutDate, double totalPrice, int numberOfRooms)
        {        
            BookingId= BookingId+1;
            Hotel = hotel;
            User = user;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            NumberOfRooms = numberOfRooms;
            TotalPrice = totalPrice;
        }


        internal void GetBookingDetails()
        {
            Console.WriteLine("\nBooking Details:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Booking Date:",-15} {CheckInDate}");
            Console.WriteLine($"{"Customer Name:",-15} {User.FirstName+" "+User.LastName}");
            Console.WriteLine($"{"Hotel:",-15} {Hotel.HotelName}");
            Console.WriteLine($"{"Check-in:",-15} {CheckInDate.ToShortDateString()}");
            Console.WriteLine($"{"Check-out:",-15} {CheckOutDate.ToShortDateString()}");
            Console.WriteLine($"{"Rooms:",-15} {NumberOfRooms}");
            Console.WriteLine($"{"Total Price:",-15} ${TotalPrice}");
            Console.WriteLine(new string('-', 50));
        }

        internal bool ProcessPayment()
        {
            Console.WriteLine("Please enter your payment details:");

            Console.WriteLine("Enter your credit card number:");
            string cardNumber = Console.ReadLine();

            Console.WriteLine("Enter the expiration date (MM/YY):");
            string expirationDate = Console.ReadLine();

            Console.WriteLine("Enter the CVV code:");
            string cvv = Console.ReadLine();

            Payment payment = new Payment(cardNumber, expirationDate, cvv, TotalPrice);

         
            return payment.ProcessPayment();
        }
    }
}
