using Application.Data.Entities;
using Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AirportService
    {
        private TicketContext ticketContext;
        public AirportService()
        {
            ticketContext = new TicketContext();
        }
        public AirportService(TicketContext context)
        {
            ticketContext = context;

        }
        public async Task AddAirportAsync(Airport airport)
        {
            var airports = await GetAirportsAsync();
            if (airports.Any(x => x.Name == airport.Name)) throw new ArgumentException("Airport already exists.");
            await ticketContext.Airports.AddAsync(airport);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Airport>> GetAirportsAsync()
        {
            var airports = await ticketContext.Airports.ToListAsync();

            if (airports.Count == 0) return new List<Airport>();

            return airports;
        }

        public async Task<Airport> GetAirportAsync(int id)
        {
            var airport = await ticketContext.Airports
                .FirstOrDefaultAsync(x => x.Id == id);

            if (airport == null)
                throw new ArgumentException("No such airport");

            return airport;
        }

        public async Task RemoveAirportAsync(int id)
        {
            var airport = await GetAirportAsync(id);

            ticketContext.Airports.Remove(airport);
            await ticketContext.SaveChangesAsync();
        }
    }
}
