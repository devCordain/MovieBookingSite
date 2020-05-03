using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieBookingSite.Data;
using System;
using System.IO;
using System.Linq;

namespace MovieBookingSite.Models
{
    public static class SeedData
    {
        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    // DB has been seeded, do nothing
                }
                else
                {
                    // Seed DB with Movies
                    context.Movies.AddRange(
                        new Movie
                        {
                            Name = "Episode IV – A New Hope",
                            Length = 121,
                            Description = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                            ReleaseDate = DateTime.Parse("1977-5-25"),
                            Director = "George Lucas",
                            ImageLarge = ReadFile("SeedData/4.jpg"),
                            ImageThumbnail = ReadFile("SeedData/e4thumb.jpg")
                        }, 
                    
                        new Movie
                        {
                            Name = "Episode V – The Empire Strikes Back",
                            Length = 124,
                            Description = "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued by Darth Vader and a bounty hunter named Boba Fett all over the galaxy.",
                            ReleaseDate = DateTime.Parse("1980-5-21"),
                            Director = "Irvin Kershner",
                            ImageLarge = ReadFile("SeedData/5.jpg"),
                            ImageThumbnail = ReadFile("SeedData/e5thumb.jpg")
                        }, 
                    
                        new Movie
                        {
                            Name = "Episode VI – Return of the Jedi",
                            Length = 132,
                            Description = "After a daring mission to rescue Han Solo from Jabba the Hutt, the Rebels dispatch to Endor to destroy the second Death Star. Meanwhile, Luke struggles to help Darth Vader back from the dark side without falling into the Emperor's trap.",
                            ReleaseDate = DateTime.Parse("1983-5-25"),
                            Director = "Richard Marquand",
                            ImageLarge = ReadFile("SeedData/6.jpg"),
                            ImageThumbnail = ReadFile("SeedData/e6thumb.jpg")
                        }, 
                    
                        new Movie
                        {
                            Name = "Episode VII – The Force Awakens",
                            Length = 135,
                            Description = "Three decades after the Empire's defeat, a new threat arises in the militant First Order. Defected stormtrooper Finn and the scavenger Rey are caught up in the Resistance's search for the missing Luke Skywalker.",
                            ReleaseDate = DateTime.Parse("2015-12-18"),
                            Director = "J. J. Abrams",
                            ImageLarge = ReadFile("SeedData/7.jpg"),
                            ImageThumbnail = ReadFile("SeedData/e7thumb.jpg")
                        }
                    );
                    context.SaveChanges();
                }
            }

            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                if (context.Salons.Any())
                {
                    // DB has been seeded, do nothing
                }
                else
                {
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

            }

            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CinemaContext>>()))
            {
                if (context.Showings.Any())
                {
                    // DB has been seeded, do nothing
                }
                else
                {
                    var movies = context.Movies.ToList();
                    var salonSmall = context.Salons.Where(x => x.Name == "Lilla Salongen").FirstOrDefault();
                    var salonLarge = context.Salons.Where(x => x.Name == "Stora Salongen").FirstOrDefault();

                    // Seed 10 days
                    for (int i = 0; i < 10; i++)
                    {
                        var showTime = 13;
                        foreach (var movie in movies)
                        {
                            var showingSmall = new Showing
                            {
                                Movie = movie,
                                Salon = salonSmall,
                                ShowTime = DateTime.Today.AddHours(showTime).AddDays(i),
                                TicketPrice = 150,
                                TicketsLeft = salonSmall.SeatsPerRow * salonSmall.Rows
                            };

                            var showingLarge = new Showing
                            {
                                Movie = movie,
                                Salon = salonLarge,
                                ShowTime = DateTime.Today.AddHours(showTime).AddDays(i),
                                TicketPrice = 125,
                                TicketsLeft = salonLarge.SeatsPerRow * salonLarge.Rows
                            };
                            context.Showings.Add(showingSmall);
                            context.Showings.Add(showingLarge);
                            showTime += 3;
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}