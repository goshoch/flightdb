using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Data
{
    public class TicketContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
            
        }
        public TicketContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string filePath = "admins.json";
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };

            var users = JsonSerializer.Deserialize<List<User>>(json, options);
            modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.HasIndex(x => x.Username).IsUnique();
                    entity.Property(x => x.Role).HasConversion<string>().HasMaxLength(20).IsRequired();
                    entity.HasData(users);

                });
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.SeatNumber).IsUnicode().HasMaxLength(5).IsRequired();
                entity.HasOne(x => x.User).WithMany(x => x.Tickets).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.Flight).WithMany(x => x.Tickets).HasForeignKey(x => x.FlightId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.FlightNumber).HasMaxLength(10).IsRequired();
                entity.Property(x => x.DepartureTime).IsRequired();
                entity.Property(x => x.ArrivalTime).IsRequired();
                entity.Property(x => x.Price).IsRequired().HasColumnType("decimal(7,2)");
                entity.HasOne(x => x.DepartureAirport).WithMany(x => x.DepartingFlights).HasForeignKey(x => x.DepartureAirportId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.ArrivalAirport).WithMany(x => x.ArrivingFlights).HasForeignKey(x => x.ArrivalAirportId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.Airline).WithMany(x => x.Flights).HasForeignKey(x => x.AirlineId).OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(x => x.FlightNumber).IsUnique();
                entity.ToTable(t => t.HasCheckConstraint("CK_Flight_Price", "[Price]>0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Flight_Times", "[ArrivalTime]>[DepartureTime]"));
                entity.ToTable(t => t.HasCheckConstraint("CK_Flight_Airports", "[ArrivalAirportId]<>[DepartureAirportId]"));
            });
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(80).IsUnicode().IsRequired();
                entity.Property(x => x.City).HasMaxLength(50).IsUnicode();
                entity.Property(x => x.Country).HasMaxLength(50).IsUnicode().IsRequired();
            });
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode();
                entity.Property(x => x.Country).IsRequired().HasMaxLength(50).IsUnicode();
                entity.HasIndex(x => x.Name).IsUnique();
            });
        }
    }
}
