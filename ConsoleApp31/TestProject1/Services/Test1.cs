using ConsoleApp31.Data;
using ConsoleApp31.Data.Entities;
using ConsoleApp31.Enums;
using ConsoleApp31.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Services
{
    internal class Test1
    {
        private DbContextOptions<TicketContext> _options;
        private ServiceCollection services = new ServiceCollection();
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TicketContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;
        }
        [TearDown]
        public void TearDown()
        {
            using (var context = new TicketContext(_options))
            {
                context?.Dispose();
            }
        }
        [Test]
        public async Task AddUserAsync_ShouldAddUser()
        {
            using var context = new TicketContext(_options);
            var service = new UserService(context);

            var user = new User
            {
                Username = "john",
                Password = "1234",
                Role = Role.Customer
            };

            await service.AddUserAsync(user);

            Assert.AreEqual(1, context.Users.Count());
        }
        [Test]
        public async Task LogIn_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            using var context = new TicketContext(_options);

            context.Users.Add(new User
            {
                Username = "john",
                Password = "1234",
                Role = Role.Customer
            });

            await context.SaveChangesAsync();

            var service = new UserService(context);

            var result = await service.LogIn("john", "1234");

            Assert.IsNotNull(result);
            Assert.AreEqual("john", result.Username);
        }
        [Test]
        public async Task AddAirlineAsync_ShouldAddAirline()
        {
            using var context = new TicketContext(_options);
            var service = new AirlineService(context);

            await service.AddAirlineAsync(new Airline
            {
                Name = "Lufthansa"
            });

            Assert.AreEqual(1, context.Airlines.Count());
        }
        [Test]
        public async Task GetFlightsCountByAirlineAsync_ShouldReturnCorrectCount()
        {
            using var context = new TicketContext(_options);

            var airline = new Airline
            {
                Name = "Lufthansa"
            };

            context.Airlines.Add(airline);
            await context.SaveChangesAsync();

            context.Flights.AddRange(
                new Flight { AirlineId = airline.Id },
                new Flight { AirlineId = airline.Id }
            );

            await context.SaveChangesAsync();

            var service = new AirlineService(context);

            var count =
                await service.GetFlightsCountByAirlineAsync(airline.Id);

            Assert.AreEqual(2, count);
        }
        [Test]
        public async Task AddFlightAsync_ShouldAddFlight()
        {
            using var context = new TicketContext(_options);

            var service = new FlightService(context);

            var flight = new Flight
            {
                DepartureAirportId = 1,
                ArrivalAirportId = 2,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2),
                Price = 100
            };

            await service.AddFlightAsync(flight);

            Assert.AreEqual(1, context.Flights.Count());
        }
        [Test]
        public async Task AddTicketAsync_ShouldAddTicket()
        {
            using var context = new TicketContext(_options);

            var service = new TicketService(context);

            var ticket = new Ticket
            {
                UserId = 1,
                FlightId = 1,
                SeatNumber = "1A",
                TicketClass = TicketClass.Economy
            };

            await service.AddTicketAsync(ticket);

            Assert.That(context.Tickets.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task AddTicketAsync_ShouldThrowException_WhenSeatIsTaken()
        {
            using var context = new TicketContext(_options);

            var service = new TicketService(context);

            var ticket1 = new Ticket
            {
                UserId = 1,
                FlightId = 1,
                SeatNumber = "1A",
                TicketClass = TicketClass.Economy
            };

            var ticket2 = new Ticket
            {
                UserId = 2,
                FlightId = 1,
                SeatNumber = "1A",
                TicketClass = TicketClass.Economy
            };

            await service.AddTicketAsync(ticket1);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.AddTicketAsync(ticket2));
        }
        [Test]
        public void GenerateSeats_ShouldGenerateCorrectNumberOfSeats()
        {
            var service = new SeatService();

            var seats = service.GenerateSeats(
                10,
                new[] { 'A', 'B', 'C' },
                new List<string>());

            Assert.AreEqual(30, seats.Count);
        }
        [Test]
        public void GenerateSeats_ShouldMarkTakenSeats()
        {
            var service = new SeatService();

            var seats = service.GenerateSeats(
                2,
                new[] { 'A', 'B' },
                new List<string> { "1A" });

            var seat = seats.First(x => x.SeatNumber == "1A");

            Assert.IsTrue(seat.IsTaken);
        }
    }
}
