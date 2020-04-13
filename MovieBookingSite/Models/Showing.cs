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

        public virtual List<Ticket> Tickets { get; set; }

        [Display(Name = "Show Time")]
        public DateTime ShowTime { get; set; }

        public int TicketPrice { get; set; }

        public int TicketsLeft()
        {
            var ticketsLeft = Salon.Rows * Salon.SeatsPerRow;
            foreach (var ticket in Tickets)
            {
                if (!ticket.Available) ticketsLeft--;
            }
            return ticketsLeft;
        }
    }
}


