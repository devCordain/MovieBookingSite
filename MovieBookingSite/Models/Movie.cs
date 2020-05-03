using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBookingSite
{
    public class Movie
    {
        public int Id { get; set; }
        [Display(Name = "Movie")]
        public string Name { get; set; }

        public int Length { get; set; }

        public string Director { get; set; }
        public string Description { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public byte[] ImageLarge { get; set; } 
        [Display(Name = "Icon")]
        public byte[] ImageThumbnail { get; set; }
    }
}
