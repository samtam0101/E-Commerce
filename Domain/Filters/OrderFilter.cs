using Domain.Enums;

namespace Domain.Filters;

public class OrderFilter:PaginationFilter
{
    public OrderStatus? OrderStatus { get; set; }
}
