using Application.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Helpers
{
    internal class TestDBFactory
    {
        public static TicketContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<TicketContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new TicketContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
