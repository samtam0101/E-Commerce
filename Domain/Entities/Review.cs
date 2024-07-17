using Domain.Enums;

namespace Domain.Entities;

public class Review
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public ReviewRating Rating { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
    
    public Product Product { get; set; }
    public User User { get; set; }
}
