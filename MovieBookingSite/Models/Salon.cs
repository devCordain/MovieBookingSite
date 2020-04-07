using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite
{
    public class Salon
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int Rows { get; set; }

        public int SeatsPerRow { get; set; }
    }
}

