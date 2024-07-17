namespace Domain.Entities;

public class Cart
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; set; }
    public List<CartItem> CartItems { get; set; }
    public List<Product> Products { get; set; }
}
