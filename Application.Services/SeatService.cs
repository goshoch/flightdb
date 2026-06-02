using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeatService
    {
        public List<Seat> GenerateSeats(
            int rows,
            char[] seatLetters,
            List<string> takenSeats)
        {
            List<Seat> seats = new List<Seat>();

            for (int row = 1; row <= rows; row++)
            {
                foreach (char letter in seatLetters)
                {
                    string seatNumber = $"{row}{letter}";

                    seats.Add(new Seat
                    {
                        SeatNumber = seatNumber,
                        IsTaken = takenSeats.Contains(seatNumber)
                    });
                }
            }

            return seats;
        }
    }
}
