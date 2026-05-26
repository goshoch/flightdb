using ConsoleApp31.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ConsoleApp31.Data;
using ConsoleApp31.Data.Entities;
using ConsoleApp31.Enums;
using ConsoleApp31.Services;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private UserService userService = new UserService();
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }
            var users=await userService.GetUsersAsync();
            if (users.Any(x=>x.Username==textBox1.Text))
            {
                MessageBox.Show("User already exists!");
                return;
            }
            User customer = new User() { Username = textBox1.Text, Password = textBox2.Text, Role = Role.Customer };
            await userService.AddUserAsync(customer);
            DialogResult = DialogResult.OK;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
