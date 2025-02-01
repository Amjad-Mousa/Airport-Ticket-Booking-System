namespace Airport_Ticket_Booking_System.Entities;

public class Booking
{
    private static int _bookingId = 1;
    public string FlightId { get; set; }
    public int PassengerId { get; set; }
    public string Class { get; set; }
    public DateTime BookingDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int BookingId { get; }

    public Booking(string flightId, int passengerId, decimal price, string @class)
    {
        BookingId = _bookingId++;
        FlightId = flightId;
        PassengerId = passengerId;
        BookingDate = DateTime.Now;
        TotalPrice = price;
    }
    
}