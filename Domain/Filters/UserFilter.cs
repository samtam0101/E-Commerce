namespace Domain.Filters;

public class UserFilter:PaginationFilter
{
    public string?  FirtName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
