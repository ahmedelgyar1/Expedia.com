using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static expedia.com.functionHelper;

namespace expedia.com
{
    public class Expedia
    {
        private IUser currentUser;
        private IUserManager userManager;

        public Expedia(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public void Start()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Welcome to Expedia.com");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\nPlease choose an option:");
            Console.WriteLine("1. Sign Up (New User)");
            Console.WriteLine("2. Sign In (Existing User)");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice (1 or 2): ");

            int click = int.Parse(Console.ReadLine());
            if (click == 1)
            {
                Register();
            }
            else if (click == 2)
            {
                SignIn();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Start();
            }
        }
        private void Register()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Sign Up");
            Console.WriteLine(new string('=', 50));
            var newUser = userManager.RegisterNewUser();
            currentUser = newUser;
            Console.WriteLine("\nRegistration successful!");
            Console.WriteLine($"Welcome, {currentUser.GetFullName()}!");
            HomePage();
        }
        private void SignIn()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Sign In");
            currentUser =userManager.SignInUser();
            if (currentUser!=null)
            {

                Console.WriteLine("\nLogin successful!");
                Console.WriteLine($"Welcome back, {currentUser.GetFullName()}!");
                HomePage();
            }
            else
            {
                Console.WriteLine("User not found. Please sign up first.");
                Start();
            }
        }
        internal void HomePage()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Welcome, {currentUser.GetFullName()}!");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Book a Stay (Hotels)");
            Console.WriteLine("2. Book a Flight");
            Console.WriteLine("3. Logout");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice (1-3): ");

            if (int.TryParse(Console.ReadLine()?.Trim(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Stays();
                        break;
                    case 2:
                        Flights();
                        break;
                    case 3:
                        Console.WriteLine("You have been logged out. Goodbye!");
                        currentUser = null;
                        Start();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        HomePage();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Returning to homepage...");
                HomePage();
            }
        }
        internal void Stays()
        {
            if (currentUser == null)
            {
                Console.WriteLine("You must sign in first.");
                Start();
                return;
            }

            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Book a Stay");
            Console.WriteLine(new string('=', 50));

            string country = GetCountry();
            string location = GetLocation();
            DateTime checkInDate = GetCheckInDate();
            DateTime checkOutDate = GetCheckOutDate();
            int numberOfRooms = GetNumberOfRooms();

            List<Hotel> filteredHotels = FilterAndSortHotels(country, location, numberOfRooms);
            DisplayAvailableHotels(filteredHotels);

            Hotel selectedHotel = SelectHotel(filteredHotels);
            double totalPrice = BookRooms(selectedHotel, numberOfRooms);

            HotelBooking booking = new HotelBooking(selectedHotel, currentUser, checkInDate, checkOutDate, totalPrice, numberOfRooms);

            while (!booking.ProcessPayment())
            {
                Console.WriteLine("Payment failed. Do you want to try again? (Y/N)");
                string choice = Console.ReadLine().ToUpper();
                if (choice != "Y")
                {
                    HomePage();
                    return;
                }
            }

            BookingManager.AddHotelBooking(booking);
            Console.WriteLine("\nBooking successful!");

            Thread.Sleep(5000);
            HomePage();
        }
        private int GetNumberOfRooms()
        {
            Console.WriteLine("Enter number of rooms:");
            int numberOfRooms;
            while (!int.TryParse(Console.ReadLine(), out numberOfRooms) || numberOfRooms <= 0)
            {
                Console.WriteLine("Please enter a valid number of rooms.");
            }
            return numberOfRooms;
        }

        private List<Hotel> FilterAndSortHotels(string country, string location, int numberOfRooms)
        {
            Console.WriteLine("How do you want to arrange hotels?");
            Console.WriteLine("1 - Recommended\n2 - Price: Low to High\n3 - Price: High to Low\n4 - Rating");
            int sortingChoice = int.Parse(Console.ReadLine());

            List<Hotel> filteredHotels = hotelList.Where(hotel => hotel.City == location && hotel.Country == country && hotel.AvailableRooms >= numberOfRooms).ToList();

            
            switch (sortingChoice)
            {
                case 1:
                    filteredHotels.Sort();
                    break;
                case 2:
                    filteredHotels.Sort((h1, h2) => h1.PricePerRoom.CompareTo(h2.PricePerRoom));
                    break;
                case 3:
                    filteredHotels.Sort((h1, h2) => h2.PricePerRoom.CompareTo(h1.PricePerRoom));
                    break;
                case 4:
                    filteredHotels.Sort((h1, h2) => h2.Rating.CompareTo(h1.Rating));
                    break;
            }

            return filteredHotels;
        }

        private void DisplayAvailableHotels(List<Hotel> filteredHotels)
        {
            Console.WriteLine("\nAvailable Hotels:");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"{"#",-5} | {"Hotel Name",-20} | {"City",-15} | {"Rating",-10} | {"Price/Night",-12} | {"Amenities"}");
            Console.WriteLine(new string('-', 100));

            int counter = 1;
            foreach (var hotel in filteredHotels)
            {
                string amenities = string.Join(", ", hotel.Amenities);
                Console.WriteLine($"{counter,-5} | {hotel.HotelName,-20} | {hotel.City,-15} | {hotel.Rating,-10} | ${hotel.PricePerRoom,-11} | {amenities}");
                counter++;
            }
            Console.WriteLine(new string('-', 100));
        }

        private Hotel SelectHotel(List<Hotel> filteredHotels)
        {
            Console.WriteLine("What hotel do you want?");
            int selectedHotelChoice = int.Parse(Console.ReadLine());
            return filteredHotels[selectedHotelChoice - 1];
        }

        private double BookRooms(Hotel selectedHotel, int numberOfRooms)
        {
            double totalPrice = 0;

            Console.WriteLine("\nAvailable Rooms:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"#",-5} | {"Room Type",-20} | {"Amenities",-30} | {"Price",-10}");
            Console.WriteLine(new string('-', 50));

            for (int i = 0; i < numberOfRooms; i++)
            {
                int roomCounter = 1;
                foreach (var room in selectedHotel.Rooms)
                {
                    if (!room.Item4)
                    {
                        Console.WriteLine($"{roomCounter,-5} | {room.Item1,-20} | {room.Item2,-30} | ${room.Item3,-9}");
                        roomCounter++;
                    }
                }

                Console.WriteLine("Choose a room:");
                int roomChoice = int.Parse(Console.ReadLine());

                int roomIndex = 0;
                
                for(int j=0;j<selectedHotel.Rooms.Count; j++)
                {
                    Tuple<string, string, double, bool> room = selectedHotel.Rooms[j];
                    if (!room.Item4&&roomIndex==roomChoice-1)
                    {
                        totalPrice+=room.Item3;
                        Tuple<string, string, double, bool> new_room = new Tuple<string, string, double, bool>(room.Item1, room.Item2, room.Item3, true);
                        selectedHotel.Rooms[j]=new_room;
                    }
                    if(!room.Item4)
                    {
                        roomIndex++;
                    }
                    
                }
        
            }

            return totalPrice;
        }
        void Flights()
        {
            if (currentUser == null)
            {
                Console.WriteLine("You must sign in first.");
                Start();
                return;
            }

            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Book a Flight");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Round Trip");
            Console.WriteLine("2. One-way");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice (1-2): ");

            if (int.TryParse(Console.ReadLine()?.Trim(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        roundTrip();
                        break;
                    case 2:
                        One_way();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Flights();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Returning to homepage...");
                HomePage();
            }
        }
        void One_way()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("One-Way Flight Booking");
            Console.WriteLine(new string('=', 50));

            string Departure = LeavingFrom();
            string destination = GetCountry();
            DateTime DepartingDate = GetCheckInDate();
            string seatclass = choosingSeatClass();

            List<Flight> flights = select_Flight(Departure, destination, DepartingDate, seatclass);
            List<Ticket> tickets = SelectTickets(flights);
            tickets = SortTickets(tickets);
            DisplayAvailableTickets(tickets);

            Ticket Ticket = SelectTicket(tickets);
            double TicketPrice = Ticket.Price;

            FlightBooking booking = new FlightBooking(currentUser, Ticket, seatclass);

            while (!booking.ProcessPayment())
            {
                Console.WriteLine("Payment failed. Do you want to try again? (Y/N)");
                string choice = Console.ReadLine().ToUpper();
                if (choice != "Y")
                {
                    HomePage();
                    return;
                }
            }

            BookingManager.AddFlightBooking(booking);
            Ticket.IsBooked = true;
            Console.WriteLine("\nBooking successful!");
            Thread.Sleep(5000);
            HomePage();
        }
        private void roundTrip()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Round-Trip Flight Booking");
            Console.WriteLine(new string('=', 50));

            string Departure = LeavingFrom();
            string destination = GetCountry();
            DateTime DepartingDate = GetCheckInDate();
            DateTime ReturningDate = GetCheckOutDate();
            string seatClass = choosingSeatClass();

            List<Flight> flights = select_Flight(Departure, destination, DepartingDate, seatClass);
            List<Ticket> tickets = SelectTickets(flights);
            tickets = SortTickets(tickets);
            DisplayAvailableTickets(tickets);

            Ticket DepartingTicket = SelectTicket(tickets);
            double TicketPrice = DepartingTicket.Price;

            seatClass = choosingSeatClass();
            flights = select_Flight(destination, Departure, ReturningDate, seatClass);
            tickets = SelectTickets(flights);
            tickets = SortTickets(tickets);
            DisplayAvailableTickets(tickets);

            Ticket returningTicket = SelectTicket(tickets);
            double TicketPrice2 = returningTicket.Price;

            FlightBooking RoundTripBooking = new FlightBooking(currentUser, DepartingTicket, seatClass, returningTicket);

            while (!RoundTripBooking.ProcessPayment())
            {
                Console.WriteLine("Payment failed. Do you want to try again? (Y/N)");
                string choice = Console.ReadLine().ToUpper();
                if (choice != "Y")
                {
                    HomePage();
                    return;
                }
            }

            BookingManager.AddFlightBooking(RoundTripBooking);
            DepartingTicket.IsBooked = true;
            returningTicket.IsBooked = true;
            Console.WriteLine("\nBooking successful!");
            HomePage();
        }

        private List<Flight> select_Flight(string Departure, string destination, DateTime DepartingDate,string seatclass)
        {
           List<Flight> result = new List<Flight>();
            for(int i =0;i<flightList.Count();i++)
            {
              
                if (flightList[i].DepartureLocation == Departure&&flightList[i].ArrivalLocation==destination&&flightList[i].seatClass.Contains(seatclass))
                {
                   result.Add( flightList[i]);
                    
                }

            }
            return result ;
        }
        private List<Ticket> SelectTickets(List<Flight> flights)
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Flight flight in flights) {
                foreach (Ticket ticket in flight.TicketList)
                {
                    ticket.DepartureTime=flight.DepartureTime;
                    ticket.ArrivalTime=flight.ArrivalTime;
                    tickets.Add(ticket);
                }

            }
            return tickets ;
        }
        private List<Ticket> SortTickets(List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Sort Tickets");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Recommended");
            Console.WriteLine("2. Price: Low to High");
            Console.WriteLine("3. Price: High to Low");
            Console.WriteLine("4. Departure (Earliest)");
            Console.WriteLine("5. Departure (Latest)");
            Console.WriteLine("6. Arrival (Earliest)");
            Console.WriteLine("7. Arrival (Latest)");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice (1-7): ");

            int sortingChoice = int.Parse(Console.ReadLine());
            switch (sortingChoice)
            {
                case 1:
                    tickets.Sort((t1, t2) => t1.TicketId.CompareTo(t2.TicketId));
                    break;
                case 2:
                    tickets.Sort((t1, t2) => t1.Price.CompareTo(t2.Price));
                    break;
                case 3:
                    tickets.Sort((t1, t2) => t2.Price.CompareTo(t1.Price));
                    break;
                case 4:
                    tickets.Sort((t1, t2) => t1.DepartureTime.CompareTo(t2.DepartureTime));
                    break;
                case 5:
                    tickets.Sort((t1, t2) => t2.DepartureTime.CompareTo(t1.DepartureTime));
                    break;
                case 6:
                    tickets.Sort((t1, t2) => t1.ArrivalTime.CompareTo(t2.ArrivalTime));
                    break;
                case 7:
                    tickets.Sort((t1, t2) => t2.ArrivalTime.CompareTo(t1.ArrivalTime));
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Recommended.");
                    tickets.Sort((t1, t2) => t1.TicketId.CompareTo(t2.TicketId));
                    break;
            }
            return tickets;
        }
        private void DisplayAvailableTickets(List<Ticket> tickets)
        {
            Console.WriteLine("\nAvailable Tickets:");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"{"#",-5} | {"Ticket ID",-10} | {"Departure",-20} | {"Arrival",-20} | {"Price",-10}");
            Console.WriteLine(new string('-', 100));

            int counter = 1;
            foreach (var ticket in tickets)
            {
                if (!ticket.IsBooked)
                {
                    Console.WriteLine($"{counter,-5} | {ticket.TicketId,-10} | {ticket.DepartureTime,-20} | {ticket.ArrivalTime,-20} | ${ticket.Price,-9}");
                    counter++;
                }
            }
            Console.WriteLine(new string('-', 100));
        }
        private string choosingSeatClass()
        {
            Console.Clear(); 
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Choose Seat Class");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Economy");
            Console.WriteLine("2. Business Class");
            Console.WriteLine("3. First Class");
            Console.WriteLine("4. Premium Economy");
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter your choice (1-4): ");

            int chosenClass = int.Parse(Console.ReadLine());
            switch (chosenClass)
            {
                case 1:
                    return "Economy";
                case 2:
                    return "Business Class";
                case 3:
                    return "First Class";
                case 4:
                    return "Premium Economy";
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Economy.");
                    return "Economy";
            }
        }

        private Ticket SelectTicket(List<Ticket> filteredTickets)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("Select Ticket");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"{"#",-5} | {"Ticket ID",-10} | {"Departure",-20} | {"Arrival",-20} | {"Price",-10}");
            Console.WriteLine(new string('-', 50));

            int counter = 1;
            foreach (var ticket in filteredTickets)
            {
                if (!ticket.IsBooked)
                {
                    Console.WriteLine($"{counter,-5} | {ticket.TicketId,-10} | {ticket.DepartureTime,-20} | {ticket.ArrivalTime,-20} | ${ticket.Price,-9}");
                    counter++;
                }
            }
            Console.WriteLine(new string('-', 50));
            Console.Write("Enter the number of the ticket you want: ");
            int selectedTicketChoice = int.Parse(Console.ReadLine());
            return filteredTickets[selectedTicketChoice - 1];
        }

    }


}
