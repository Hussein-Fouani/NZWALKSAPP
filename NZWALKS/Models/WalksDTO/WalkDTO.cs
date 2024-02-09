using NZWALKS.Models.DTO;

namespace NZWALKS.Models.WalksDTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
        public string? WalkImageURI { get; set; }
        public Difficulty difficulty { get; set; }
        public RegionDTO regionsdto { get; set; }
    }
}
