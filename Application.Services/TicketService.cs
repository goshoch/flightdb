using Application.Data;
using Application.Data.Entities;
using Application.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketService
    {
        private TicketContext ticketContext;
        public TicketService()
        {
            ticketContext = new TicketContext();
        }
        public TicketService(TicketContext context)
        {
            ticketContext = context;

        }
        public async Task AddTicketAsync(Ticket ticket)
        {
            bool seatTaken = await ticketContext.Tickets
                .AnyAsync(x => x.FlightId == ticket.FlightId &&
                               x.SeatNumber == ticket.SeatNumber);

            if (seatTaken)
                throw new ArgumentException("Seat number is taken");

            await ticketContext.Tickets.AddAsync(ticket);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var tickets = await ticketContext.Tickets.ToListAsync();

            if (tickets.Count == 0)
                throw new ArgumentException("List is empty");

            return tickets;
        }

        public async Task<Ticket> GetTicketAsync(int id)
        {
            var ticket = await ticketContext.Tickets
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
                throw new ArgumentException("No such ticket");

            return ticket;
        }

        public async Task RemoveTicketAsync(int id)
        {
            var ticket = await GetTicketAsync(id);

            ticketContext.Tickets.Remove(ticket);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(int id)
        {
            return await ticketContext.Tickets
                .Include(x => x.User)
                .Include(x => x.Flight)
                .ThenInclude(x=>x.DepartureAirport)
                .Include(x=>x.Flight)
                .ThenInclude(x=>x.ArrivalAirport)
                .Where(x => x.UserId == id)
                .ToListAsync();
        }
    }
}
