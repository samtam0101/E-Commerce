namespace Domain.DTOs.CartDto;

public class GetCartsDto
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}
