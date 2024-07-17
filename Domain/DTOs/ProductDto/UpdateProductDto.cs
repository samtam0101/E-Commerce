using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.ProductDto;

public class UpdateProductDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile ImageUrl { get; set; }
}
