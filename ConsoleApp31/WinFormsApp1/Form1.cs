using ConsoleApp31.Data.Entities;
using ConsoleApp31.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ConsoleApp31.Services;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            DialogResult result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show("User added successfully!");
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            var userService = new UserService();
            if (!await userService.LogIn(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("Invalid credentials!"); return;
            }
            TicketContext context = new TicketContext();
            User user = await context.Users.FirstOrDefaultAsync(u => u.Password == textBox2.Text && u.Username == textBox1.Text);
            if (user == null)
            {
                MessageBox.Show("User not found!");
                return;
            }
            if (user.Role == 0)
            {
                AdminForm form3 = new AdminForm();
                form3.ShowDialog();
            }
            else
            {

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
