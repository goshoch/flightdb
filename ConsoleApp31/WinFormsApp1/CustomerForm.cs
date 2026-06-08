using Application.Data;
using Application.Data.Entities;
using Application.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controls;

namespace WinFormsApp1
{
    public partial class CustomerForm : Form
    {
        private readonly TicketService ticketService;
        private readonly FlightService flightService;
        private readonly TicketContext context;
        private User currentUser;
        private List<Flight> flightlist;
        public CustomerForm(User user)
        {
            InitializeComponent();
            context = new TicketContext();
            ticketService = new TicketService(context);
            flightService = new FlightService(context);
            currentUser = user;
        }

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            await LoadFlightsAsync();
        }

        private async Task LoadFlightsAsync()
        {
            try
            {
                flightlist = await flightService.GetFlightsAsync();
            }
            catch (Exception ex)
            {
                return;
            }
            dataGridView1.DataSource = flightlist.Select(p => new
            {
                p.Id,
                DepartureAirport = p.DepartureAirport.Name,
                ArrivalAirport = p.ArrivalAirport.Name,
                p.DepartureTime,
                p.ArrivalTime,
                Airline = p.Airline.Name,
                p.Price
            }).ToList();
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].Visible = false;
            }

            if (!dataGridView1.Columns.Contains("addToCartButtonColumn"))
            {
                var addToCartButtonColumn = new DataGridViewButtonColumn();
                addToCartButtonColumn.Name = "addToCartButtonColumn";
                addToCartButtonColumn.HeaderText = "Add to Cart";
                addToCartButtonColumn.Text = "Add to Cart";
                addToCartButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(addToCartButtonColumn);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            TicketViewForm form = new TicketViewForm(currentUser, context);
            form.ShowDialog();
            await LoadFlightsAsync();
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["addToCartButtonColumn"].Index && e.RowIndex >= 0)
            {
                flightlist = await flightService.GetFlightsAsync();
                var flightId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                var flight = flightlist.First(p => p.Id == flightId);
                List<string> takenseats = flight.Tickets.Select(x => x.SeatNumber).ToList();

                TicketPurchaseForm form = new TicketPurchaseForm(flight, currentUser, context);
                form.ShowDialog();
                await LoadFlightsAsync();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
