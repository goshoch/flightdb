using Application.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<Ticket> Tickets=new List<Ticket>();
        public override string ToString()
        {
            return $"{Username} - {Role}";
        }
    }
}
