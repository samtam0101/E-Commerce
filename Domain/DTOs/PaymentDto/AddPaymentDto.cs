using Domain.Enums;

namespace Domain.DTOs.PaymentDto;

public class AddPaymentDto
{
    public int OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
