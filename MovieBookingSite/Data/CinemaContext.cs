using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite.Data
{
    public class CinemaContext :DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Salon> Salons { get; set; }

        public DbSet<Showing> Showings { get; set; }
    }
}
