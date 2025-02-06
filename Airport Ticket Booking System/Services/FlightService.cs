using System.Collections.Immutable;
using System.Text;
using Airport_Ticket_Booking_System.DTOs;
using Airport_Ticket_Booking_System.Entities;
using Airport_Ticket_Booking_System.Helper;

namespace Airport_Ticket_Booking_System.Services;

using Airport_Ticket_Booking_System.Entities;

public class FlightService
{
    public static Flight? GetFlightById(string id)
    {
        return CsvHelperService.ReadFromCsv<Flight>("jetbrains://rd/navigate/reference?project=Airport%20Ticket%20Booking%20System&path=Airport%20Ticket%20Booking%20System%2FData%2FFlight.csv")
            .FirstOrDefault(f => f.Id == id);
    }

    public static string GetFlightDetails(string id)
    {
        var flight = GetFlightById(id);
        return flight != null ? flight.ToString() : "Flight not found.";
    }

    public static string UpdateFlight(Flight updatedFlight)
    {
        var flight = GetFlightById(updatedFlight.Id);
        if (flight == null)
            return "Flight not found.";

        flight.DepartureCountry = updatedFlight.DepartureCountry ?? flight.DepartureCountry;
        flight.DestinationCountry = updatedFlight.DestinationCountry ?? flight.DestinationCountry;
        flight.DepartureDate =
            updatedFlight.DepartureDate != default ? updatedFlight.DepartureDate : flight.DepartureDate;
        flight.ArrivalDate = updatedFlight.ArrivalDate != default ? updatedFlight.ArrivalDate : flight.ArrivalDate;
        flight.Price = updatedFlight.Price != 0 ? updatedFlight.Price : flight.Price;
        flight.DepartureAirport = updatedFlight.DepartureAirport ?? flight.DepartureAirport;
        flight.DestinationAirport = updatedFlight.DestinationAirport ?? flight.DestinationAirport;
        flight.Class = updatedFlight.Class ?? flight.Class;

        CsvHelperService.AddToCsv("jetbrains://rd/navigate/reference?project=Airport%20Ticket%20Booking%20System&path=Airport%20Ticket%20Booking%20System%2FData%2FFlight.csv", flight);

        return "Flight updated successfully.";
    }

    public string CheckSeatAvailability(string flightId, int seatNumber)
    {
        var flight = GetFlightById(flightId);
        if (flight.SeatAvailability.TryGetValue(seatNumber, out var value))
        {
            return value == "Available" ? "Seat is available." : "Seat is booked.";
        }
        else
        {
            return "Seat number not found.";
        }
    }

    public string ReserveSeat( string flightId, int seatNumber)
    {
        var flight = GetFlightById(flightId);
        var seatStatus = CheckSeatAvailability(flightId, seatNumber);

        if (seatStatus == "Seat is available.")
        {
            flight.SeatAvailability[seatNumber] = "Reserved";
            return "Seat booked successfully.";
        }

        return seatStatus;
    }

    public string CancelSeatReservation(string flightId, int seatNumber)
    {
        var flight = GetFlightById(flightId);
        var seatStatus = CheckSeatAvailability(flight.Id, seatNumber);

        if (seatStatus == "Seat is booked.")
        {
            flight.SeatAvailability[seatNumber] = "Available";
            return "Seat unbooked successfully.";
        }

        return seatStatus;
    }

    public static string CalculateFlightDuration(string flightId)
    {
        var flight = GetFlightById(flightId);
        TimeSpan duration = flight.ArrivalDate - flight.DepartureDate;
        string durationStr = $"{duration.Hours}h {duration.Minutes}m";
        return durationStr;
    }

    public string ListPassengers(string flightId)
    {
        var flight = GetFlightById(flightId);
        if (flight.PassengersList.Count == 0)
        {
            return "No passengers booked for this flight.";
        }

        var passengerDetails = new StringBuilder();
        foreach (var passenger in flight.PassengersList)
        {
            passengerDetails.AppendLine($"Passenger ID: {passenger.Id}, Name: {passenger.Name}");
        }

        return passengerDetails.ToString();
    }
}