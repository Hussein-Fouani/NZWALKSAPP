using System.ComponentModel.DataAnnotations;

namespace NZWALKS.Models.WalksDTO
{
    public class AddWalkDTO
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name has to have a min of 30  Characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Description has to have a min of 100  Characters")]
        public string Description { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Start has to have a min of 30  Characters")]
        public string Region { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Start has to have a min of 30  Characters")]
        public string Difficulty { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageURI { get; set; }
        public Guid DiffuclityId { get; set; }
        public Guid RegionId { get; set; }


    }
}
