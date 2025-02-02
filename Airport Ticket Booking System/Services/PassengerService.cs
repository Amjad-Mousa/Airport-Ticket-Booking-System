using Airport_Ticket_Booking_System.Entities;
using System.Linq;

namespace Airport_Ticket_Booking_System.Services
{
    public class PassengerService
    {
        public void AddBooking(Passenger passenger, Booking booking)
        {
            passenger.Bookings.Add(booking);
        }

        public bool CancelBooking(Passenger passenger, int bookingId)
        {
            var booking = passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                passenger.Bookings.Remove(booking);
                return true;
            }

            return false;
        }

        public void ViewBookings(Passenger passenger)
        {
            if (passenger.Bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            foreach (var booking in passenger.Bookings)
            {
                Console.WriteLine(
                    $"Booking ID: {booking.BookingId}, Flight ID: {booking.FlightId}, Date: {booking.BookingDate}, Class: {booking.BookingClass}, Total Price: {booking.TotalPrice} USD");
            }
        }

        public Booking? GetBookingById(Passenger passenger, int bookingId)
        {
            return passenger.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }
    }
}