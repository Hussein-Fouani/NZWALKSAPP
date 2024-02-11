using System.ComponentModel.DataAnnotations;

namespace NZWALKS.Models
{
    public class ImageUploadRequestDto
    {
        [Required]
        public FormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
