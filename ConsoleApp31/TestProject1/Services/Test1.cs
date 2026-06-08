using Application.Data;
using Application.Data.Entities;
using Application.Data.Enums;
using Application.Services;
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
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            using var context = new TicketContext(_options);
            var service = new UserService(context);
            await service.AddUserAsync(new User { Username = "user1", Password = "password" });
            await service.AddUserAsync(new User { Username = "user2", Password = "password" });

            var users = await service.GetUsersAsync();

            Assert.AreEqual(2, users.Count);
        }

        [Test]
        public async Task GetUserAsync_ShouldReturnCorrectUser()
        {
            using var context = new TicketContext(_options);
            var service = new UserService(context);
            var user = new User { Username = "user1", Password = "password" };
            await service.AddUserAsync(user);

            var result = await service.GetUserAsync(user.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("user1", result.Username);
        }
        [Test]
        public async Task AddAirlineAsync_ShouldAddAirline()
        {
            using var context = new TicketContext(_options);
            var service = new AirlineService(context);

            await service.AddAirlineAsync(new Airline
            {
                Name = "Lufthansa",
                Country = "Germany"
            });

            Assert.AreEqual(1, context.Airlines.Count());
        }
        [Test]
        public async Task GetAirlinesAsync_ShouldReturnAllAirlines()
        {
            using var context = new TicketContext(_options);
            var service = new AirlineService(context);
            await service.AddAirlineAsync(new Airline { Name = "Lufthansa", Country = "Germany" });
            await service.AddAirlineAsync(new Airline { Name = "Turkish Airlines", Country = "Turkey" });

            var airlines = await service.GetAirlinesAsync();

            Assert.AreEqual(2, airlines.Count);
        }
        [Test]
        public async Task GetFlightsCountByAirlineAsync_ShouldReturnCorrectCount()
        {
            using var context = new TicketContext(_options);

            var airline = new Airline
            {
                Name = "Lufthansa",
                Country = "Germany"
            };

            context.Airlines.Add(airline);
            await context.SaveChangesAsync();

            context.Flights.AddRange(
    new Flight
    {
        FlightNumber = "LH100",
        AirlineId = airline.Id
    },
    new Flight
    {
        FlightNumber = "LH200",
        AirlineId = airline.Id
    }
);

            await context.SaveChangesAsync();

            var service = new AirlineService(context);

            var count =
                await service.GetFlightsCountByAirlineAsync(airline.Id);

            Assert.AreEqual(2, count);
        }
        [Test]
        public async Task AddAirportAsync_ShouldAddAirport()
        {
            using var context = new TicketContext(_options);
            var service = new AirportService(context);
            var airport = new Airport { Name = "Test Airport", City = "Test City", Country = "Test Country" };
            await service.AddAirportAsync(airport);

            Assert.AreEqual(1, context.Airports.Count());
        }

        [Test]
        public async Task GetAirportsAsync_ShouldReturnAllAirports()
        {
            using var context = new TicketContext(_options);
            var service = new AirportService(context);
            await service.AddAirportAsync(new Airport { Name = "Test Airport 1", City = "Test City 1", Country = "Test Country 1" });
            await service.AddAirportAsync(new Airport { Name = "Test Airport 2", City = "Test City 2", Country = "Test Country 2" });

            var airports = await service.GetAirportsAsync();

            Assert.AreEqual(2, airports.Count);
        }

        [Test]
        public async Task GetAirportAsync_ShouldReturnCorrectAirport()
        {
            using var context = new TicketContext(_options);
            var service = new AirportService(context);
            var airport = new Airport { Name = "Test Airport", City = "Test City", Country = "Test Country" };
            await service.AddAirportAsync(airport);

            var result = await service.GetAirportAsync(airport.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test Airport", result.Name);
        }
        [Test]
        public async Task AddFlightAsync_ShouldAddFlight()
        {
            using var context = new TicketContext(_options);

            var service = new FlightService(context);

            var airline = new Airline { Name = "Test Airline", Country = "Germany" };
            context.Airlines.Add(airline);
            await context.SaveChangesAsync();

            var flight = new Flight
            {
                AirlineId = airline.Id,
                FlightNumber = "LH100",
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
        public async Task GetFlightsAsync_ShouldReturnAllFlights()
        {
            using var context = new TicketContext(_options);
            var service = new FlightService(context);
            var airline = new Airline { Name = "Test Airline", Country = "Germany" };
            context.Airlines.Add(airline);
            await context.SaveChangesAsync();

            await service.AddFlightAsync(new Flight { AirlineId = airline.Id, FlightNumber = "LH100", DepartureAirportId = 1, ArrivalAirportId = 2, DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(2), Price = 100 });
            await service.AddFlightAsync(new Flight { AirlineId = airline.Id, FlightNumber = "LH200", DepartureAirportId = 1, ArrivalAirportId = 2, DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(2), Price = 200 });
            
            var flights = await service.GetFlightsAsync();

            Assert.AreEqual(2, flights.Count);
        }

        [Test]
        public async Task GetFlightAsync_ShouldReturnCorrectFlight()
        {
            using var context = new TicketContext(_options);
            var service = new FlightService(context);
            var airline = new Airline { Name = "Test Airline", Country = "Germany" };
            context.Airlines.Add(airline);
            await context.SaveChangesAsync();

            var flight = new Flight { AirlineId = airline.Id, FlightNumber = "LH100", DepartureAirportId = 1, ArrivalAirportId = 2, DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now.AddHours(2), Price = 100 };
            await service.AddFlightAsync(flight);

            var result = await service.GetFlightAsync(flight.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("LH100", result.FlightNumber);
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
        public async Task GetTicketsAsync_ShouldReturnAllTickets()
        {
            using var context = new TicketContext(_options);
            var service = new TicketService(context);
            await service.AddTicketAsync(new Ticket { UserId = 1, FlightId = 1, SeatNumber = "1A", TicketClass = TicketClass.Economy });
            await service.AddTicketAsync(new Ticket { UserId = 2, FlightId = 1, SeatNumber = "1B", TicketClass = TicketClass.Business });

            var tickets = await service.GetTicketsAsync();

            Assert.AreEqual(2, tickets.Count);
        }

        [Test]
        public async Task GetTicketAsync_ShouldReturnCorrectTicket()
        {
            using var context = new TicketContext(_options);
            var service = new TicketService(context);
            var ticket = new Ticket { UserId = 1, FlightId = 1, SeatNumber = "1A", TicketClass = TicketClass.Economy };
            await service.AddTicketAsync(ticket);
            var addedTicket = await context.Tickets.SingleAsync(t => t.SeatNumber == "1A");

            var result = await service.GetTicketAsync(addedTicket.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("1A", result.SeatNumber);
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
