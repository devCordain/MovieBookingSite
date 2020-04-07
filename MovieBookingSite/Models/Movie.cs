﻿using System;
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
        public string Name { get; set; }

        public double Length { get; set; }

        public string Description { get; set; }

        [Display(Name = "Age Limit")]
        public int? AgeLimit { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}