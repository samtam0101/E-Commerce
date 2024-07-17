namespace Domain.DTOs.CartItemDto;

public class AddCartItemDto
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
