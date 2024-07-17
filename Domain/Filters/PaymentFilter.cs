using Domain.Enums;

namespace Domain.Filters;

public class PaymentFilter:PaginationFilter
{
    public PaymentMethod? PaymentMethod { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
}
