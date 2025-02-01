using System.Diagnostics.CodeAnalysis;
using Airport_Ticket_Booking_System.DTOs;
using Airport_Ticket_Booking_System.Entities;
using Airport_Ticket_Booking_System.Helper;

namespace Airport_Ticket_Booking_System.Services;

public enum FlightClass
{
    Economy = 100,
    Business = 120,
    FirstClass = 150
}

public class BookingService
{
    public bool DeleteBooking(BookingDto bookingDto)
    {
        return CsvHelperService.DeleteFromCsv<Booking>("Booking.csv", b => b.BookingId == bookingDto.Id);
    }

    public bool ModifyBooking(Booking booking, FlightClass newClass)
    {
        if (!IsPassengerExists(booking.PassengerId))
        {
            return false;
        }

        booking.Class = newClass.ToString();
        booking.TotalPrice = CalculatePriceWithClass(booking, newClass);
        return true;
    }


    public decimal CalculatePriceWithClass(Booking booking, FlightClass newClass)
    {
        return (decimal)newClass;
    }

    public Booking? GetBookingById(BookingDto bookingDto)
    {
        var booking = CsvHelperService.ReadFromCsv<Booking>("Booking.csv")
            .FirstOrDefault(b => b.BookingId == bookingDto.Id);

        return booking;
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