﻿namespace Explorer.Tours.API.Dtos
{
    public class CheckpointPreviewDto
    {
        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Pictures { get; set; }
        public double RequiredTimeInSeconds { get; set; }
    }
}
