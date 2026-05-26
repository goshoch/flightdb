using ConsoleApp31.Data;
using ConsoleApp31.Data.Entities;
using ConsoleApp31.Enums;
using ConsoleApp31.Services;
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
            var airports = await airportService.GetAirportsAsync();
            var airlines = await airlineService.GetAirlinesAsync();
            var users = await userService.GetUsersAsync();

            comboBox1.DataSource = airports;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            comboBox2.DataSource = airports;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "Name";

            comboBox1.BindingContext = new BindingContext();
            comboBox2.BindingContext = new BindingContext();
            comboBox3.DataSource = airlines;
            comboBox3.ValueMember = "Id";
            comboBox3.DisplayMember = "Name";

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
            comboBox4.DataSource = await userService.GetUsersAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.IsNullOrEmpty() || textBox4.Text.IsNullOrEmpty()) { MessageBox.Show("Fill out the details."); return; }
            Airline airline = new Airline() { Name = textBox3.Text, Country = textBox4.Text };
            await airlineService.AddAirlineAsync(airline);
            MessageBox.Show("Airline added!");
            textBox3.Clear(); textBox4.Clear();
            var airlines= await airlineService.GetAirlinesAsync();
            comboBox3.DataSource =airlines;
            comboBox3.ValueMember = "Id";
            comboBox3.DisplayMember = "Name";

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
            var airports = await airportService.GetAirportsAsync();
            comboBox1.DataSource = airports;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            comboBox2.DataSource = airports;
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "Name";

            comboBox1.BindingContext = new BindingContext();
            comboBox2.BindingContext = new BindingContext();
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

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
