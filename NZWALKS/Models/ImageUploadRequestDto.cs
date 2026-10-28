using System.ComponentModel.DataAnnotations;

namespace NZWALKS.Models
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
