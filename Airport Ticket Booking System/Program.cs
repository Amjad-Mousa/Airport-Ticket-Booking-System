using System;
using Airport_Ticket_Booking_System.DTOs;
using Airport_Ticket_Booking_System.Entities;
using Airport_Ticket_Booking_System.Services;

class Program
{
    static BookingService bookingService = new BookingService();
    static FlightService flightService = new FlightService();
    static PassengerService passengerService = new PassengerService();

    static void Main(string[] args)
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Airport Ticket Booking System");
            Console.WriteLine("1. Create Booking");
            Console.WriteLine("2. Update Booking");
            Console.WriteLine("3. Delete Booking");
            Console.WriteLine("4. View All Bookings");
            Console.WriteLine("5. View Flight Details");
            Console.WriteLine("6. Check Seat Availability");
            Console.WriteLine("7. Reserve Seat");
            Console.WriteLine("8. Cancel Seat Reservation");
            Console.WriteLine("9. View Passengers for a Flight");
            Console.WriteLine("10. Exit");

            Console.Write("Please select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateBooking();
                    break;
                case "2":
                    UpdateBooking();
                    break;
                case "3":
                    DeleteBooking();
                    break;
                case "4":
                    ViewAllBookings();
                    break;
                case "5":
                    ViewFlightDetails();
                    break;
                case "6":
                    CheckSeatAvailability();
                    break;
                case "7":
                    ReserveSeat();
                    break;
                case "8":
                    CancelSeatReservation();
                    break;
                case "9":
                    ViewPassengersForFlight();
                    break;
                case "10":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void CreateBooking()
    {
        Console.Write("Enter Flight ID: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Passenger ID: ");
        int passengerId = int.Parse(Console.ReadLine());

        Console.Write("Enter Class (Economy, Business, FirstClass): ");
        FlightClass flightClass = (FlightClass)Enum.Parse(typeof(FlightClass), Console.ReadLine(), true);

        Flight flight = FlightService.GetFlightById(flightId);

        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Booking newBooking = new Booking(flight.Id, passengerId, flightClass);

        bookingService.AddBooking(newBooking);
        Console.WriteLine("Booking created successfully!");
    }

    static void UpdateBooking()
    {
        Console.Write("Enter Booking ID to update: ");
        int bookingId = int.Parse(Console.ReadLine());

        var booking = BookingService.GetBookingById(bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
            return;
        }

        Console.WriteLine("Enter new details for the booking...");
        // Implement your update logic here
        Console.WriteLine("Booking updated successfully!");
    }

    static void DeleteBooking()
    {
        Console.Write("Enter Booking ID to delete: ");
        int bookingId = int.Parse(Console.ReadLine());

        var bookingDto = new BookingDto { Id = bookingId };
        bool result = bookingService.DeleteBooking(bookingDto);

        if (result)
        {
            Console.WriteLine("Booking deleted successfully.");
        }
        else
        {
            Console.WriteLine("Booking not found.");
        }
    }

    static void ViewAllBookings()
    {
        Console.Write("Enter Passenger ID to view bookings: ");
        int passengerId = int.Parse(Console.ReadLine());

        var bookings = bookingService.GetAllBookings(passengerId);
        if (bookings.Count > 0)
        {
            foreach (var booking in bookings)
            {
                Console.WriteLine(booking);
            }
        }
        else
        {
            Console.WriteLine("No bookings found.");
        }
    }

    static void ViewFlightDetails()
    {
        Console.Write("Enter Flight ID to view details: ");
        string flightId = Console.ReadLine();

        string flightDetails = FlightService.GetFlightDetails(flightId);
        Console.WriteLine(flightDetails);
    }

    static void CheckSeatAvailability()
    {
        Console.Write("Enter Flight ID to check seat availability: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Seat Number: ");
        int seatNumber = int.Parse(Console.ReadLine());

        string seatStatus = flightService.CheckSeatAvailability(flightId, seatNumber);
        Console.WriteLine(seatStatus);
    }

    static void ReserveSeat()
    {
        Console.Write("Enter Flight ID to reserve a seat: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Seat Number: ");
        int seatNumber = int.Parse(Console.ReadLine());

        string result = flightService.ReserveSeat(flightId, seatNumber);
        Console.WriteLine(result);
    }

    static void CancelSeatReservation()
    {
        Console.Write("Enter Flight ID to cancel seat reservation: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Seat Number: ");
        int seatNumber = int.Parse(Console.ReadLine());

        string result = flightService.CancelSeatReservation(flightId, seatNumber);
        Console.WriteLine(result);
    }

    static void ViewPassengersForFlight()
    {
        Console.Write("Enter Flight ID to view passengers: ");
        string flightId = Console.ReadLine();

        string passengers = flightService.ListPassengers(flightId);
        Console.WriteLine(passengers);
    }
}
