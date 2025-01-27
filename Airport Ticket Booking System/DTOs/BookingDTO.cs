namespace Airport_Ticket_Booking_System.DTOs;

public class BookingDto
{
    FlightDto flightDTO = new FlightDto();
    PassengerDTO passengerDTO = new PassengerDTO();
    PaymentDto paymentDTO = new PaymentDto();
    public string? BookingStatus { get; set; }
    public DateTime BookingDate { get; set; }
    public int BookingId { get; set; }
    
}