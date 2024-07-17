namespace Domain.Filters;

public class ProductFilter:PaginationFilter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
