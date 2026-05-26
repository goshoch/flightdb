using ConsoleApp31.Data.Entities;
using ConsoleApp31.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp31.Services
{
    public class AirlineService
    {
        private TicketContext ticketContext;
        public AirlineService()
        {
            ticketContext = new TicketContext();
        }
        public async Task AddAirlineAsync(Airline airline)
        {
            await ticketContext.Airlines.AddAsync(airline);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Airline>> GetAirlinesAsync()
        {
            var airlines = await ticketContext.Airlines.ToListAsync();

            if (airlines.Count == 0) airlines=new List<Airline>();

            return airlines;
        }

        public async Task<Airline> GetAirlineAsync(int id)
        {
            var airline = await ticketContext.Airlines
                .FirstOrDefaultAsync(x => x.Id == id);

            if (airline == null)
                throw new ArgumentException("No such airline");

            return airline;
        }

        public async Task RemoveAirlineAsync(int id)
        {
            var airline = await GetAirlineAsync(id);

            ticketContext.Airlines.Remove(airline);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<int> GetFlightsCountByAirlineAsync(int airlineId)
        {
            var airline = await ticketContext.Airlines
                .Include(x => x.Flights)
                .FirstOrDefaultAsync(x => x.Id == airlineId);

            if (airline == null)
                throw new ArgumentException("No such airline");

            return airline.Flights.Count;
        }

        public async Task<Airline> GetAirlineWithMostFlightsAsync()
        {
            var airline = await ticketContext.Airlines
                .Include(x => x.Flights)
                .OrderByDescending(x => x.Flights.Count)
                .FirstOrDefaultAsync();

            if (airline == null)
                throw new ArgumentException("List is empty");

            return airline;
        }

        public async Task<List<Flight>> GetFlightsByAirlineAfterDateAsync(int airlineId, DateTime date)
        {
            return await ticketContext.Flights
                .Where(x => x.AirlineId == airlineId &&
                            x.DepartureTime > date)
                .ToListAsync();
        }
    }
}
