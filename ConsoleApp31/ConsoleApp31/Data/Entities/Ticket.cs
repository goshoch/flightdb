using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp31.Enums;

namespace ConsoleApp31.Data.Entities
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public TicketClass TicketClass { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public decimal? Price { get; set; }
        public User User { get; set; }  
        public Flight Flight { get; set; }
        public override string ToString()
        {
            return $"{SeatNumber} - {User.Username} - {Flight.FlightNumber}";
        }
    }
}
