namespace Airport_Ticket_Booking_System.DTOs;

public class PaymentDto
{
    public string? PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}