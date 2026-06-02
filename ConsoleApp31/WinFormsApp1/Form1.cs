using Application.Data.Entities;
using Application.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.Data.Enums;

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
            textBox1.Clear(); textBox2.Clear();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            var userService = new UserService();
            var user =await userService.LogIn(textBox1.Text, textBox2.Text);
            if (user==null )
            {
                MessageBox.Show("Invalid credentials!"); return;
            }
            if (user.Role == Role.Admin)
            {
                AdminForm form3 = new AdminForm();
                form3.Show();
            }
            else
            {
                CustomerForm form4 = new CustomerForm(user);
                form4.Show();
            }
            textBox1.Clear();textBox2.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
