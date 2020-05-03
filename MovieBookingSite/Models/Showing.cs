using Microsoft.AspNetCore.Authentication;
using MovieBookingSite.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite
{
    public class Showing
    {
        public int Id { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Salon Salon { get; set; }

        public int TicketsLeft { get; set; }

        [Display(Name = "Show Time")]
        public DateTime ShowTime { get; set; }

        public int TicketPrice { get; set; }

        public virtual IList<Ticket> Tickets { get; set; }

        public void SeedSeats()
        {
            for (int row = 1; row < Salon.Rows; row++)
            {
                for (int seat = 1; seat < Salon.SeatsPerRow; seat++)
                {
                    Tickets.Add(new Ticket { Row = row, Seat = seat });
                }
            }
        }
    }
}


