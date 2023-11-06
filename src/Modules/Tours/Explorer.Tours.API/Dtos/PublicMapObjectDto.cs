﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicMapObjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureURL { get; set; }
        public string? Category { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string? Status { get; set; }
    }
}
