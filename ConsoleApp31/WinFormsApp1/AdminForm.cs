using Application.Data;
using Application.Data.Entities;
using Application.Data.Enums;
using Application.Services;
using Microsoft.IdentityModel.Tokens;
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
    public partial class AdminForm : Form
    {
        private UserService userService = new UserService();
        private AirlineService airlineService = new AirlineService();
        private AirportService airportService = new AirportService();
        private FlightService flightService = new FlightService();
        private TicketContext context = new TicketContext();
        public AdminForm()
        {
            InitializeComponent();
            this.Load += AdminForm_Load;
        }
        private async void AdminForm_Load(object sender, EventArgs e)
        {
            await RefreshAdminDataAsync();
        }

        private async Task RefreshAdminDataAsync()
        {
            await RefreshAirportsAsync();
            await RefreshAirlinesAsync();
            await RefreshFlightsAsync();
            await RefreshUsersAsync();
        }

        private async Task RefreshAirportsAsync()
        {
            var airports = await airportService.GetAirportsAsync();

            comboBox1.DataSource = airports;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            comboBox2.DataSource = airports;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "Name";

            comboBox1.BindingContext = new BindingContext();
            comboBox2.BindingContext = new BindingContext();

            comboBox7.DataSource = airports.ToList();
            comboBox7.ValueMember = "Id";
            comboBox7.DisplayMember = "Name";
        }

        private async Task RefreshAirlinesAsync()
        {
            var airlines = await airlineService.GetAirlinesAsync();

            comboBox3.DataSource = airlines;
            comboBox3.ValueMember = "Id";
            comboBox3.DisplayMember = "Name";

            comboBox6.DataSource = airlines.ToList();
            comboBox6.ValueMember = "Id";
            comboBox6.DisplayMember = "Name";
        }

        private async Task RefreshFlightsAsync()
        {
            try
            {
                comboBox5.DataSource = await flightService.GetFlightsAsync();
                comboBox5.ValueMember = "Id";
                comboBox5.DisplayMember = "FlightNumber";
            }
            catch (ArgumentException)
            {
                comboBox5.DataSource = new List<Flight>();
            }
        }

        private async Task RefreshUsersAsync()
        {
            var users = await userService.GetUsersAsync();
            comboBox4.DataSource = users;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Select an item");
                return;
            }
            User user = (User)comboBox4.SelectedItem;
            if (user.Role == Role.Admin)
            {
                MessageBox.Show("Cannot delete an admin");
                return;
            }
            await userService.RemoveUserAsync(user.Id);
            MessageBox.Show("User deleted successfully");
            await RefreshUsersAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.IsNullOrEmpty() || textBox4.Text.IsNullOrEmpty()) { MessageBox.Show("Fill out the details."); return; }
            Airline airline = new Airline() { Name = textBox3.Text, Country = textBox4.Text };
            await airlineService.AddAirlineAsync(airline);
            MessageBox.Show("Airline added!");
            textBox3.Clear(); textBox4.Clear();
            await RefreshAirlinesAsync();

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.IsNullOrEmpty() || textBox6.Text.IsNullOrEmpty() || textBox7.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Fill out the details.");
                return;
            }
            Airport airport = new Airport() { Name = textBox5.Text, City = textBox6.Text, Country = textBox7.Text };
            await airportService.AddAirportAsync(airport);
            MessageBox.Show("Airport added!");
            textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
            await RefreshAirportsAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IsNullOrEmpty() || textBox2.Text.IsNullOrEmpty() || textBox8.Text.IsNullOrEmpty() || textBox9.Text.IsNullOrEmpty() || comboBox1.SelectedValue == null || comboBox2.SelectedValue == null || comboBox3.SelectedValue == null)
            {
                MessageBox.Show("Fill out the details");
                return;
            }
            Flight flight = new Flight()
            {
                FlightNumber = textBox1.Text,
                Price = decimal.Parse(textBox2.Text),
                DepartureTime = DateTime.Parse(textBox8.Text),
                ArrivalTime = DateTime.Parse(textBox9.Text),
                DepartureAirportId = (int)comboBox1.SelectedValue,
                ArrivalAirportId = (int)comboBox2.SelectedValue,
                AirlineId = (int)comboBox3.SelectedValue
            };
            try
            {
                await flightService.AddFlightAsync(flight);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
            MessageBox.Show("Flight added");
            textBox1.Clear(); textBox2.Clear(); textBox8.Clear(); textBox9.Clear();
            await RefreshFlightsAsync();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Select a flight");
                return;
            }

            Flight flight = (Flight)comboBox5.SelectedItem;

            try
            {
                await flightService.RemoveFlightAsync(flight.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Flight deleted successfully");
            await RefreshFlightsAsync();
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem == null)
            {
                MessageBox.Show("Select an airline");
                return;
            }

            Airline airline = (Airline)comboBox6.SelectedItem;

            try
            {
                await airlineService.RemoveAirlineAsync(airline.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Airline deleted successfully");
            await RefreshAirlinesAsync();
            await RefreshFlightsAsync();
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem == null)
            {
                MessageBox.Show("Select an airport");
                return;
            }

            Airport airport = (Airport)comboBox7.SelectedItem;

            try
            {
                await airportService.RemoveAirportAsync(airport.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Airport deleted successfully");
            await RefreshAirportsAsync();
            await RefreshFlightsAsync();
        }
    }
}
