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
    public class FlightService
    {
        private TicketContext ticketContext;
        public FlightService()
        {
            ticketContext = new TicketContext();
        }
        public FlightService(TicketContext context)
        {
            ticketContext = context;

        }
        public void AddFlight(Flight flight)
        {
            ticketContext.Flights.Add(flight);
            ticketContext.SaveChanges();
        }
        public async Task AddFlightAsync(Flight flight)
        {
            if (flight.ArrivalTime < flight.DepartureTime) { throw new ArgumentException("Arrival time cannot be before departure time"); }
            if (flight.DepartureAirportId == flight.ArrivalAirportId) { throw new ArgumentException("Cannot fly to the same airport"); }
            await ticketContext.Flights.AddAsync(flight);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            var flights = await ticketContext.Flights.Include(x=>x.Airline).Include(x=>x.DepartureAirport).Include(x=>x.ArrivalAirport).Include(x=>x.Tickets).ToListAsync();

            if (flights.Count == 0)
                throw new ArgumentException("List is empty");

            return flights;
        }

        public async Task<Flight> GetFlightAsync(int id)
        {
            var flight = await ticketContext.Flights
                .FirstOrDefaultAsync(x => x.Id == id);

            if (flight == null)
                throw new ArgumentException("No such flight");

            return flight;
        }

        public async Task RemoveFlightAsync(int id)
        {
            var flight = await GetFlightAsync(id);

            ticketContext.Flights.Remove(flight);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Flight>> GetFlightsFromCityAfterDateAsync(string city, DateTime date)
        {
            return await ticketContext.Flights
                .Include(x => x.DepartureAirport)
                .Where(x => x.DepartureAirport.City == city &&
                            x.DepartureTime > date)
                .ToListAsync();
        }

        public async Task<List<Flight>> GetFlightsToCityAfterDateAsync(string city, DateTime date)
        {
            return await ticketContext.Flights
                .Include(x => x.ArrivalAirport)
                .Where(x => x.ArrivalAirport.City == city &&
                            x.ArrivalTime > date)
                .ToListAsync();
        }

        public async Task<List<Flight>> GetFlightsByAirlineAsync(int airlineId)
        {
            return await ticketContext.Flights
                .Where(x => x.AirlineId == airlineId)
                .ToListAsync();
        }

        public async Task<Flight> GetMostExpensiveFlightAsync()
        {
            var flight = await ticketContext.Flights
                .OrderByDescending(x => x.Price)
                .FirstOrDefaultAsync();

            if (flight == null)
                throw new ArgumentException("List is empty");

            return flight;
        }
    }
}
