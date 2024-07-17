namespace Domain.Filters;

public class CategoryFilter:PaginationFilter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
