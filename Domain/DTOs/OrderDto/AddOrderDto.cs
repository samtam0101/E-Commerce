using Domain.Enums;

namespace Domain.DTOs.OrderDto;

public class AddOrderDto
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int ShippingAddressId { get; set; }  
    public int BillingAddressId { get; set; }
}
