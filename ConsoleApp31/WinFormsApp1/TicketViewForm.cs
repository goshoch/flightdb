using Application.Data.Entities;
using Application.Data.Enums;
using Application.Services;
using Microsoft.VisualBasic.Logging;
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
    public partial class TicketViewForm : Form
    {
        private TicketService ticketService = new TicketService();
        private User User { get; set; }
        public TicketViewForm()
        {
            InitializeComponent();

        }
        public TicketViewForm(User user)
        {
            InitializeComponent();
            User = user;
            dataGridView1.Columns.Add("FlightNumber", "Flight");
            dataGridView1.Columns.Add("From", "From");
            dataGridView1.Columns.Add("To", "To");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Seat", "Seat");
            dataGridView1.Columns.Add("Class", "Class");
            LoadTickets();

        }
        private async void LoadTickets()
        {
            var tickets = await ticketService.GetTicketsByUserIdAsync(User.Id);

            dataGridView1.Rows.Clear();

            foreach (var t in tickets)
            {
                dataGridView1.Rows.Add(
                    t.Flight.FlightNumber,
                    t.Flight.DepartureAirport.Name,
                    t.Flight.ArrivalAirport.Name,
                    t.Price,
                    t.SeatNumber,
                    t.TicketClass
                );
            }
        }
        private void TicketViewForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
