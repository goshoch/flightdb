using ConsoleApp31.Data;
using ConsoleApp31.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp31.Services
{
    public class UserService
    {
        private TicketContext ticketContext;
        public UserService()
        {
            ticketContext=new TicketContext();
           
        }
        public async Task<User> LogIn(string name, string password)
        {
            var user = await ticketContext.Users.FirstOrDefaultAsync(u => u.Username == name && u.Password == password);
            return user;
        }
        public async Task AddUserAsync(User User)
        {
            await ticketContext.Users.AddAsync(User);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var Users = await ticketContext.Users.ToListAsync();

            if (Users.Count == 0) return new List<User>();

            return Users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var User = await ticketContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (User == null)
                throw new ArgumentException("No such User");

            return User;
        }

        public async Task UpdateUserAsync(User User)
        {
            var UserToUpdate = await ticketContext.Users
                .FirstOrDefaultAsync(x => x.Id == User.Id);

            if (UserToUpdate == null)
                throw new ArgumentException("No such User");


            await ticketContext.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(int id)
        {
            var User = await GetUserAsync(id);

            ticketContext.Users.Remove(User);
            await ticketContext.SaveChangesAsync();
        }

        public async Task<int> GetFlightsCountByUserAsync(int id)
        {
            var User = await ticketContext.Users
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (User == null)
                throw new ArgumentException("No such User");

            return User.Tickets.Count;
        }

        public async Task<int> GetFlightsCountByUserForAirlineAsync(int UserId, int airlineId)
        {
            var User = await ticketContext.Users
                .Include(x => x.Tickets)
                .ThenInclude(x => x.Flight)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            if (User == null)
                throw new ArgumentException("No such User");

            return User.Tickets
                .Count(x => x.Flight.AirlineId == airlineId);
        }
    }
}
