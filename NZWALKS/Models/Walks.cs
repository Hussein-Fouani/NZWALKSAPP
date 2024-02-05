namespace NZWALKS.Models
{
    public class Walks
    {
        public Guid Id{get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURI { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        public Regions regions { get; set; }
        public Difficulty difficulty { get; set; }

    }
}
