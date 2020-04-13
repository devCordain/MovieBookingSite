using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieBookingSite.Data;
using System;
using System.Linq;

namespace MovieBookingSite.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "Episode IV – A New Hope",
                        Length = 121,
                        Description = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                        ReleaseDate = DateTime.Parse("1977-5-25"),
                        Director = "George Lucas"
                    }, 
                    
                    new Movie
                    {
                        Name = "Episode V – The Empire Strikes Back",
                        Length = 124,
                        Description = "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued by Darth Vader and a bounty hunter named Boba Fett all over the galaxy.",
                        ReleaseDate = DateTime.Parse("1980-5-21"),
                        Director = "Irvin Kershner"
                    }, 
                    
                    new Movie
                    {
                        Name = "Episode VI – Return of the Jedi",
                        Length = 132,
                        Description = "After a daring mission to rescue Han Solo from Jabba the Hutt, the Rebels dispatch to Endor to destroy the second Death Star. Meanwhile, Luke struggles to help Darth Vader back from the dark side without falling into the Emperor's trap.",
                        ReleaseDate = DateTime.Parse("1983-5-25"),
                        Director = "Richard Marquand"
                    }, 
                    
                    new Movie
                    {
                        Name = "Episode VII – The Force Awakens",
                        Length = 135,
                        Description = "Three decades after the Empire's defeat, a new threat arises in the militant First Order. Defected stormtrooper Finn and the scavenger Rey are caught up in the Resistance's search for the missing Luke Skywalker.",
                        ReleaseDate = DateTime.Parse("2015-12-18"),
                        Director = "J. J. Abrams"
                    }
                );
                context.SaveChanges();
            }

            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                if (context.Salons.Any())
                {
                    return;
                }

                context.Salons.AddRange(
                    new Salon
                    {
                        Name = "Lilla Salongen",
                        Description = "En intim filmupplevelse",
                        Rows = 5,
                        SeatsPerRow =  10
                    },

                    new Salon
                    {
                        Name = "Stora Salongen",
                        Description = "Var en i mängden",
                        Rows = 10,
                        SeatsPerRow = 10
                    }
                );
                context.SaveChanges();
            }

            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                if (context.Showings.Any())
                {
                    return;
                }

                var movies = context.Movies.ToList();
                var salon = context.Salons.Where(x => x.Name == "Lilla Salongen").FirstOrDefault();
                var showTime = 6;
                foreach (var movie in movies)
                {
                    context.Showings.Add(
                        new Showing
                        {
                            Movie = movie,
                            Salon = salon,
                            ShowTime = DateTime.Today.AddHours(showTime),
                            TicketPrice = 100
                        }
                        ) ;
                    showTime += 3;
                }
                context.SaveChanges();
            }
        }
    }
}