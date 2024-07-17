using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDto;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare("Password"), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}