namespace Airport_Ticket_Booking_System.Entities;

public class Payment
{
   
    public string? PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; } 
}