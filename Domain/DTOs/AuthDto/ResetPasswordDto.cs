using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDto;

public class ResetPasswordDto
{
    [MaxLength]
    public string Code { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare("Password") , DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
