using expedia.com;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using static expedia.com.functionHelper;
namespace expedia.com
{
    public static class DataGenerator
    {
        internal static List<Flight> flights = new List<Flight>();
        internal static List<Hotel> hotels = new List<Hotel>();
        public static void startGenrateFlights()
        {


            string[] lines = File.ReadAllLines("flights_with_tickets.txt");

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue; 

                string[] parts = line.Split(", TicketList: [");
                string flightData = parts[0];
                string ticketsData = parts.Length > 1 ? parts[1].TrimEnd(']') : "";

               
                string[] flightDetails = flightData.Split(", ");
                Flight flight = new Flight
                {
                    FlightId = int.Parse(flightDetails[0].Split(": ")[1]),
                    Airline = flightDetails[1].Split(": ")[1],
                    DepartureLocation = flightDetails[2].Split(": ")[1],
                    ArrivalLocation = flightDetails[3].Split(": ")[1],
                    DepartureTime = DateTime.Parse(flightDetails[4].Split(": ")[1]),
                    ArrivalTime = DateTime.Parse(flightDetails[5].Split(": ")[1]),
                    seatClass = flightDetails[6].Split(": ")[1],
                    TicketList = new List<Ticket>()
                };

                if (!string.IsNullOrWhiteSpace(ticketsData))
                {
                    string[] ticketsArray = ticketsData.Split("), (");
                    foreach (string ticket in ticketsArray)
                    {
                        string cleanedTicket = ticket.Replace("(", "").Replace(")", "");
                        string[] ticketDetails = cleanedTicket.Split(", ");

                        Ticket newTicket = new Ticket
                        {
                            TicketId = int.Parse(ticketDetails[0].Split(": ")[1]),
                            Price = double.Parse(ticketDetails[1].Split(": ")[1]),
                            IsBooked = bool.Parse(ticketDetails[2].Split(": ")[1])
                        };

                        flight.TicketList.Add(newTicket);
                    }
                }

                flights.Add(flight);
            }

          
            functionHelper.flightList = flights;

        }
        internal static void LoadHotels()
        {
           
            
            try
            {
                string[] lines = File.ReadAllLines("hotels.txt");
                foreach (string line in lines)
                {
                    var hotel = ParseHotel(line);
                    if (hotel != null)
                    {
                        hotels.Add(hotel);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading hotels: {ex.Message}");
            }

            hotelList=hotels;
        }

        private static Hotel ParseHotel(string line)
        {
            try
            {
                var hotel = new Hotel();
                var parts = line.Split(',');
                foreach (var part in parts)
                {
                    var keyValue = part.Split(':');
                    string key = keyValue[0];
                    string value = keyValue[1];

                    switch (key)
                    {
                        case "HotelName ":
                            hotel.HotelName = value;
                            break;
                        case "Country":
                            hotel.Country = value;
                            break;
                        case "City":
                            hotel.City = value;
                            break;
                        case "AvailableRooms":
                            hotel.AvailableRooms = int.Parse(value);
                            break;
                        case "PricePerRoom":
                            hotel.PricePerRoom = double.Parse(value);
                            break;
                        case "Rating":
                            hotel.Rating = double.Parse(value);
                            break;
                        case "Amenities":
                            hotel.Amenities = new List<string>(value.Split('|'));
                            break;
                        case "Rooms":
                            hotel.Rooms = ParseRooms(value);
                            break;
                    }
                }
                return hotel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing hotel: {ex.Message}");
                return null;
            }
        }

        private static List<Tuple<string, string, double, bool>> ParseRooms(string roomsData)
        {
            var rooms = new List<Tuple<string, string, double, bool>>();
            var roomEntries = roomsData.Split(';');
            foreach (var entry in roomEntries)
            {
                var roomParts = entry.Split('|');
                var room = new Tuple<string, string, double, bool>(
                    roomParts[0], // Room Type
                    roomParts[1], // Amenities
                    double.Parse(roomParts[2]), // Price
                    bool.Parse(roomParts[3]) // Booked
                );
                rooms.Add(room);
            }
            return rooms;
        }
        

    }
  
}   