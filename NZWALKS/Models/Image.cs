using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic.CompilerServices;

namespace NZWALKS.Models;

public class Image
{
    public Guid Id { get; set; }
    [NotMapped]
    public required IFormFile File { get; set; }

    public string FileName { get; set; }
    public string? FileDescription { get; set; }
    public string FileExtension { get; set; }
    public long FileSizeInBytes { get; set; }
    public string filePath { get; set; }
    
}