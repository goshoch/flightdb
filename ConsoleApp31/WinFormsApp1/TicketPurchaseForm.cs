using ConsoleApp31.Data.Entities;
using ConsoleApp31.Enums;
using ConsoleApp31.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class TicketPurchaseForm : Form
    {
        private readonly SeatService seatService=new SeatService();
        public Flight Flight { get; set; }
        public User Customer { get; set; }
        private readonly TicketService ticketService = new TicketService();
        public TicketPurchaseForm()
        {
            InitializeComponent();
        }
        public TicketPurchaseForm(Flight flight, User user)
        {
            InitializeComponent();
            Flight = flight;
            Customer = user;
            List<string> takenseats = flight.Tickets.Select(x => x.SeatNumber).ToList();
            var seats = seatService.GenerateSeats(
        10,
        new char[] { 'A', 'B', 'C', 'D', 'E', 'F' },
                takenseats
                );

            seatPickerControl1.LoadSeats(seats);
            seatPickerControl1.SeatSelected += SeatPickerControl1_SeatSelected;
        }
        private void SeatPickerControl1_SeatSelected(Seat seat)
        {
            labelSelectedSeat.Text = $"Selected: {seat.SeatNumber}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedSeat = seatPickerControl1.SelectedSeat;

            if (selectedSeat == null)
            {
                MessageBox.Show("Please select a seat");
                return;
            }

            Ticket ticket = new Ticket
            {
                SeatNumber = selectedSeat.SeatNumber,
                FlightId = Flight.Id,
                UserId = Customer.Id,
                Price = Flight.Price,
                TicketClass = (TicketClass)selectedSeat.Class
            };
            if (ticket.TicketClass == TicketClass.FirstClass) ticket.Price *= 2;
            else if (ticket.TicketClass == TicketClass.Business) ticket.Price *= 1.5m;

            ticketService.AddTicketAsync(ticket);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
