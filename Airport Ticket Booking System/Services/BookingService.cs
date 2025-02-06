using System.Diagnostics.CodeAnalysis;
using Airport_Ticket_Booking_System.DTOs;
using Airport_Ticket_Booking_System.Entities;
using Airport_Ticket_Booking_System.Helper;

namespace Airport_Ticket_Booking_System.Services;

public enum FlightClass
{
    Economy = 10,
    Business = 50,
    FirstClass = 100
}

public class BookingService
{
    public bool DeleteBooking(BookingDto bookingDto)
    {
        return CsvHelperService.DeleteFromCsv<Booking>("Booking.csv", b => b.BookingId == bookingDto.Id);
    }

    public void AddBooking(Booking booking)
    {
        CsvHelperService.AddToCsv("Booking.csv", booking);
    }
    
    public static string UpdateBooking(Booking updatedBooking)
    {
        var booking = GetBookingById(updatedBooking.BookingId);

        if (booking == null)
            return "Booking not found.";

        booking.FlightId = updatedBooking.FlightId ?? booking.FlightId;
        booking.BookingClass =
            updatedBooking.BookingClass != default ? updatedBooking.BookingClass : booking.BookingClass;
        booking.Status = updatedBooking.Status ?? booking.Status;

        if (updatedBooking.BookingClass != default)
        {
            BookingService.CalculatePriceWithClass(booking, updatedBooking.BookingClass);
        }

        CsvHelperService.AddToCsv("Bookings.csv", booking);

        return "Booking updated successfully.";
    }


    public static void CalculatePriceWithClass(Booking booking, FlightClass newClass)
    {
        booking.TotalPrice = (booking.TotalPrice - (decimal)booking.BookingClass) + (decimal)newClass;
    }

    public static Booking? GetBookingById(int bookingId)
    {
        return CsvHelperService.ReadFromCsv<Booking>("Booking.csv")
            .FirstOrDefault(b => b.BookingId == bookingId);
    }


    public List<Booking> GetAllBookings(int passengerId)
    {
        if (IsPassengerExists(passengerId))
            return CsvHelperService.ReadFromCsv<Booking>("Booking.csv")
                .Where(booking => booking.PassengerId == passengerId)
                .ToList();
        return [];
    }

    public List<Booking> GetFillterdBookings(BookingDto bookingDto, string Fillter)
    {
        if (Fillter.ToLower().Equals("flightid"))
            return FillterByFlightId(bookingDto);

        if (Fillter.ToLower().Equals("passengerid"))
            return FillterByPassengerId(bookingDto);
        return new List<Booking>();
    }


    private List<Booking> FillterByPassengerId(BookingDto bookingDto)
    {
        return CsvHelperService.ReadFromCsv<Booking>("Booking.csv")
            .Where(booking => booking.PassengerId == bookingDto.PassengerId).ToList();
    }

    private List<Booking> FillterByFlightId(BookingDto bookingDto)
    {
        return CsvHelperService.ReadFromCsv<Booking>("Booking.csv")
            .Where<Booking>(booking => booking.FlightId == bookingDto.FlightId).ToList();
    }

    private static bool IsPassengerExists(int passengerId)
    {
        var passengers = CsvHelperService.ReadFromCsv<Passenger>("Passenger.csv");

        var passenger = passengers.FirstOrDefault(p => p.Id == passengerId);

        return passenger != null;
    }
}