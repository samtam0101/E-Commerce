namespace Domain.Entities;

public class Wishlist
{
    public int WishlistId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; set; }
    public List<WishlistItem> WishlistItems { get; set; }
}
