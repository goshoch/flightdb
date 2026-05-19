using ConsoleApp31.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31.Data.Entities
{
    public class Seat
    {
        public string SeatNumber { get; set; }
        public bool IsTaken { get; set; }
        public TicketClass? Class { get; set; }
    }
}
