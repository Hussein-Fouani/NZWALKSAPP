﻿namespace NZWALKS.Models.DTO
{
    public class UpdateWalkDTO
    {
            public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURI { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
