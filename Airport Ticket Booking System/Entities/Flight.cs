namespace Airport_Ticket_Booking_System.Entities;

public class Flight
{
    public required string Id { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int Price { get; set; }
    public string? DepartureAirport { get; set; }
    public string? DestinationAirport { get; set; }
    public string? Class { get; set; }
    public List<bool> AvailableSeats { get; set; } = new List<bool>();
}