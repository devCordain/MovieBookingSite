using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieBookingSite;
using MovieBookingSite.Data;
using QRCoder;

namespace MovieBookingSite.Controllers
{
    public class ShowingsController : Controller
    {
        private readonly CinemaContext _context;

        public ShowingsController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? sortOrder = null, string? movieFilter = null, DateTime? dateFilter = null)
        {
            List<Showing> showings;
            
            if (!String.IsNullOrEmpty(movieFilter))
            {
                showings = await _context.Showings.Include("Movie").Include("Salon").Where(x => x.Movie.Name == movieFilter && x.ShowTime > DateTime.Now).ToListAsync();
            }
            else
            {
                showings = await _context.Showings.Include("Movie").Include("Salon").Where(x => x.ShowTime > DateTime.Now).ToListAsync();
            }
        
            if(dateFilter != null)
            {
                showings = showings.Where(x => x.ShowTime.Date == dateFilter).ToList();
            }

            if (sortOrder == "tickets")
            {
                return View(showings.OrderBy(x => x.TicketsLeft));
            }
            else if (sortOrder == "name")
            {
                return View(showings.OrderBy(x => x.Movie.Name));
            }
            else
            {
                return View(showings.OrderBy(x => x.ShowTime));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int id)
        {
            var showing = await _context.Showings.Include("Movie").Include("Salon").FirstOrDefaultAsync(x => x.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    int.TryParse(Request.Form["numberOfTickets"].ToString(), out int result);
                    showing.TicketsLeft -= result;
                    _context.Update(showing);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Confirmation", "Showings", new { numberOfTickets = result, showTime = showing.ShowTime, salon = showing.Salon.Name, movie = showing.Movie.Name });
                }
                catch (Exception)
                {
                    if (!ShowingExists(showing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(showing);
        }

        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var showing = await _context.Showings.Include("Movie").Include("Salon").FirstOrDefaultAsync(x => x.Id == id);
            if (showing == null)
            {
                return NotFound();
            }
            return View(showing);
        }

        public async Task<IActionResult> Confirmation(int numberOfTickets, DateTime showTime, string salon, string movie)
        {
            if (numberOfTickets < 1 || numberOfTickets > 12)
            {
                    return NotFound();
            }
            ViewBag.NumberOfTickets = numberOfTickets;
            ViewBag.ShowTime = showTime;
            ViewBag.Salon = salon;
            ViewBag.Movie = movie;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("Tickets: " + numberOfTickets + ". At: " + showTime + ", " + salon + ". For movie: " + movie, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            Byte[] image = stream.ToArray();

            ViewBag.QRImage = Convert.ToBase64String(image);

            return View();
        }

        private bool ShowingExists(int id)
        {
            return _context.Showings.Any(e => e.Id == id);
        }
    }
}
