namespace Domain.Filters;

public class AddressFilter:PaginationFilter
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
}
