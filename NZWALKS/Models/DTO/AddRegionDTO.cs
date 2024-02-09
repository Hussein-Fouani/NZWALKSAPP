using System.ComponentModel.DataAnnotations;

namespace NZWALKS.Models.DTO
{
    public class AddRegionDTO
    {
        [Required]
        [MinLength(2,ErrorMessage ="Code has to have a min of 2 Characters")]
        [MaxLength(3,ErrorMessage = "Code has to have a max of 3 Characters")] 
        public string Code { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Name has to have a min of 30  Characters")]
        public string Name { get; set; }
        public string? RegionImageURI { get; set; }
    }
}
