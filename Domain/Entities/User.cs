namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string  FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Code { get; set; }
    public DateTimeOffset CodeTime { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegistrationDate { get; set; }

    public List<Address> Addresses { get; set; }
    public Cart Cart { get; set; }
    public List<Order> Orders { get; set; }
    public List<Review> Reviews { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public List<Wishlist> Wishlists { get; set; }
}