using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDto;

public class LoginDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
