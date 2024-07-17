using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDto;

public class ChangePasswordDto
{
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Compare("NewPassword"), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
