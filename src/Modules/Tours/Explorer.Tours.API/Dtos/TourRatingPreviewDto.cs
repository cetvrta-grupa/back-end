﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourRatingPreviewDto
    {
        public long Id { get; set; }    
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int TouristId { get; set; }
        public DateTime TourDate { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string>? ImageNames { get; set; }
    }
}
