using Domain.Enums;

namespace Domain.Filters;

public class ReviewFilter:PaginationFilter
{
    public string? Comment { get; set; }
    public ReviewRating? Rating { get; set; }
}
