namespace Domain.DTOs.WishListDto;

public class GetWishlistsDto
{
    public int WishlistId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}
