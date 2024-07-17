using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int ShippingAddressId { get; set; }  // Foreign key to the Address table
    public int BillingAddressId { get; set; }   // Foreign key to the Address table

    public User User { get; set; }
    public Address ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
}