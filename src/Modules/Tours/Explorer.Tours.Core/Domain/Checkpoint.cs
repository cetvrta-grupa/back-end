﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class Checkpoint : Entity
    {
        public long TourId { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<string> Pictures { get; init; }

        public Checkpoint(long tourId, double longitude, double latitude, string name, string description, List<string> pictures)
        {
            if (tourId == 0) throw new ArgumentException("Invalid Tour ID");
            TourId = tourId;
            Longitude = longitude;
            Latitude = latitude;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            if (pictures.Count() > 0)
                Pictures = pictures ?? throw new ArgumentNullException(nameof(pictures));
            else throw new ArgumentException("Invalid Picture");
        }
    }
}
