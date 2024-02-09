 namespace NZWALKS.Models.WalksDTO
{
    public class AddWalkDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
        public string Difficulty { get; set; }
        public string? WalkImageURI { get; set; }
        public Guid DiffuclityId { get; set; }
        public Guid RegionId { get; set; }


    }
}
