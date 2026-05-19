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

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private TicketContext context = new TicketContext();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

            User customer = new User() { Password = textBox1.Text, Username = textBox2.Text, Role = Role.Customer };
            context.Users.Add(customer);
            context.SaveChanges();
            DialogResult = DialogResult.OK;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
