using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31.Data.Entities
{
    public partial class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Flight> DepartingFlights { get;set; }
        public ICollection<Flight> ArrivingFlights { get;set; }
    }
}
