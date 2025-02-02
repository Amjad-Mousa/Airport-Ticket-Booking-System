using Airport_Ticket_Booking_System.Helper;
using Airport_Ticket_Booking_System.Services;

namespace Airport_Ticket_Booking_System.Entities;

public class Flight
{
    public required string Id { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public decimal Price { get; set; }
    public string? DepartureAirport { get; set; }
    public string? DestinationAirport { get; set; }
    public string? Class { get; set; }
    private Dictionary<int, string> AvilabileSeats = new Dictionary<int, string>();
    private List<Passenger> Passengers = new List<Passenger>();

    public Flight(string flightId, decimal price, string? departureCountry, string? destinationCountry, DateTime departureDate, DateTime arrivalDate, string? departureAirport, string? destinationAirport, string? flightClass)
    {
        Id = flightId;
        Price = price;
        DepartureCountry = departureCountry;
        DestinationCountry = destinationCountry;
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        DepartureAirport = departureAirport;
        DestinationAirport = destinationAirport;
        Class = flightClass;
        AvilabileSeats = new Dictionary<int, string>();  
        Passengers = new List<Passenger>();  
    }

    public List<Passenger> PassengersList
    {
        get { return Passengers; } 
    }

    public Dictionary<int, string> SeatAvailability
    {
        get { return AvilabileSeats; }
    }


    public Flight(string flightId, decimal i)
    {
        Id = flightId;
        Price = i;
    }


    public override string ToString()
    {
        var durationStr = FlightService.CalculateFlightDuration(this.Id);
        int availableSeatsCount = SeatAvailability.Count(seat => seat.Value == "Available");
        int availablePassengers = PassengersList.Count();

        return $"Flight ID: {Id}\n" +
               $"Departure Country: {DepartureCountry}\n" +
               $"Destination: {DestinationCountry}\n" +
               $"Departure Time: {DepartureDate}\n" +
               $"Arrival Time: {ArrivalDate}\n" +
               $"Duration: {durationStr}\n" +
               $"Price: {Price} USD\n" +
               $"Class: {Class}\n" +
               $"Available Seats: {availableSeatsCount}\n" +
               $"Available Passengers: {availablePassengers}\n";
    }
}