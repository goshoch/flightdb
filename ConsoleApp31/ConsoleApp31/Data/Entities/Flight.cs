using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31.Data.Entities
{
    public partial class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int AirlineId { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public Airline Airline { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public override string ToString()
        {
            return $"{FlightNumber} {DepartureAirport.Name} - {ArrivalAirport.Name}";
        }
    }
}
