namespace Domain.DTOs.CategoryDto;

public class UpdateCategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
