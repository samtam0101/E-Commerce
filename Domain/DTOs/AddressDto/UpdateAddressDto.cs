namespace Domain.DTOs.AddressDto;

public class UpdateAddressDto
{
    public int AddressId { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}
