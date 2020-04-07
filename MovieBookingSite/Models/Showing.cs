using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite
{
    public class Showing
    {
        public int Id { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Salon Salon { get; set; }

        [Display(Name = "Show Time")]
        public DateTime ShowTime { get; set; }

        public int TicketPrice { get; set; }
    }
}

