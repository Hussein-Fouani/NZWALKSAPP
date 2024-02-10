using System.ComponentModel.DataAnnotations;

namespace NZWALKS.Models.DTO;

public class RegisterDTO
{
    [Required]
    [StringLength(20, MinimumLength = 8)]
    [DataType(DataType.EmailAddress)]
    public string UserName { get; set; }

    [Required]
    [Range(15, 40)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string[] Roles { get; set; }
}