namespace Domain.DTOs.CartDto;

public class UpdateCartDto
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}
