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
using ConsoleApp31.Enums;

namespace WinFormsApp1.Controls
{
    public partial class SeatPickerControl : UserControl
    {
        public Seat SelectedSeat { get; private set; }

        public event Action<Seat> SeatSelected;
        public SeatPickerControl()
        {
            InitializeComponent();
        }

        private void SeatPickerControl_Load(object sender, EventArgs e)
        {


        }
        public void LoadSeats(List<Seat> seats)
        {
            panelSeats.Controls.Clear();

            int columnsBeforeGap = 3;

            int index = 0;

            foreach (Seat seat in seats)
            {
                Button btn = new Button();

                btn.Width = 45;
                btn.Height = 45;

                btn.Text = seat.SeatNumber;
                btn.Tag = seat;

                int row = index / 6;
                int col = index % 6;

                int gap = col >= columnsBeforeGap ? 30 : 0;

                btn.Left = 20 + (col * 50) + gap;
                btn.Top = 20 + (row * 50);

                if (seat.IsTaken)
                {
                    btn.BackColor = Color.Red;
                    btn.Enabled = false;
                }
                else
                {
                    btn.BackColor = Color.LightGreen;
                    btn.Click += Seat_Click;
                }

                panelSeats.Controls.Add(btn);

                index++;
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            foreach (Control control in panelSeats.Controls)
            {
                if (control is Button button && button.Enabled)
                {
                    button.BackColor = Color.LightGreen;
                }
            }

            Button clicked = (Button)sender;

            clicked.BackColor = Color.DodgerBlue;

            SelectedSeat = (Seat)clicked.Tag;
            if(SelectedSeat.SeatNumber.StartsWith("1"))SelectedSeat.Class = TicketClass.FirstClass;
            else if (SelectedSeat.SeatNumber.StartsWith("2") || SelectedSeat.SeatNumber.StartsWith("3")) SelectedSeat.Class = TicketClass.Business;
            else SelectedSeat.Class = TicketClass.Economy;

            SeatSelected?.Invoke(SelectedSeat);
        }

        private void panelSeats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

