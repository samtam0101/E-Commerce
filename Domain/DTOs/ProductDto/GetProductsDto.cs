namespace Domain.DTOs.ProductDto;

public class GetProductsDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string ImageUrl { get; set; }
}
