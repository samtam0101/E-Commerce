namespace Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string ImageUrl { get; set; }

    public List<CartItem> CartItems { get; set; }
    public Category Category { get; set; }
    public List<Review> Reviews { get; set; }
    public List<WishlistItem> WishlistItems { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}