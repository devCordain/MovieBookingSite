using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite
{
    public class Ticket
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int SeatOnRow { get; set; }

        public bool Available { get; set; }
    }
}

