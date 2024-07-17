namespace Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public User User { get; set; }
    public List<Order> ShippingAddress { get; set; }
    public List<Order> BillingAddress { get; set; }
}
