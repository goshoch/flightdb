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

            var groupedSeats = seats
                .GroupBy(s =>
                    int.Parse(new string(
                        s.SeatNumber.TakeWhile(char.IsDigit).ToArray())))
                .OrderBy(g => g.Key);

            int visualRow = 0;

            foreach (var rowGroup in groupedSeats)
            {
                int colsInRow = rowGroup.Key == 1 ? 4 : 6;

                int panelWidth = panelSeats.Width;
                int rowWidth = colsInRow * 50;

                int startX = (panelWidth - rowWidth) / 2;

                int extraGap = 0;

                if (visualRow >= 1)
                    extraGap += 15;

                if (visualRow >= 3)
                    extraGap += 15;

                int col = 0;

                foreach (Seat seat in rowGroup.Take(colsInRow))
                {
                    Button btn = new Button();

                    btn.Width = 45;
                    btn.Height = 45;

                    btn.Text = seat.SeatNumber;
                    btn.Tag = seat;

                    btn.Left = startX + (col * 50);
                    btn.Top = 20 + (visualRow * 50) + extraGap;

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

                    col++;
                }

                visualRow++;
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
            if(SelectedSeat.SeatNumber.StartsWith("1")&&SelectedSeat.SeatNumber.Length==3)SelectedSeat.Class = TicketClass.FirstClass;
            else if (SelectedSeat.SeatNumber.StartsWith("2") || SelectedSeat.SeatNumber.StartsWith("3") && SelectedSeat.SeatNumber.Length == 3) SelectedSeat.Class = TicketClass.Business;
            else SelectedSeat.Class = TicketClass.Economy;

            SeatSelected?.Invoke(SelectedSeat);
        }

        private void panelSeats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

