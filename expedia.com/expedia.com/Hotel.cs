using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace expedia.com
{
    internal class Hotel
    {

        internal string HotelName {  get; set; }
        internal string Country { get; set; }
        internal string City {  get; set; }
        internal List<Tuple<string ,string,double,bool >> Rooms { get; set; }
        internal int AvailableRooms {  get; set; }
        internal double PricePerRoom {  get; set; }
        internal double Rating {  get; set; }
        internal List<string> Amenities {  get; set; }


        
        internal void AddAmenity(string Amenity)
        {
            Amenities.Add(Amenity);
        }
        internal void AddRooms(string properties, string aminities,double price,bool booked)
        {
            Rooms.Add(Tuple.Create(properties,aminities,price,booked));
        }
        internal string GetBookingDetails()
        {
            string amenities = string.Join(", ", Amenities);
            return $"Hotel: {HotelName}, Location: {City}, Rating: {Rating}/5, Available Rooms: {AvailableRooms}, Amenities: {amenities}";
        }
        

    }
}
