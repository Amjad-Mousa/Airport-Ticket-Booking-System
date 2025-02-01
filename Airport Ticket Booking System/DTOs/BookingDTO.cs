namespace Airport_Ticket_Booking_System.DTOs;
using System;
using System.Collections.Generic;
using Airport_Ticket_Booking_System.Entities;
public class BookingDto
{
    public decimal Id { get; set; }
    public int PassengerId { get; set; }
    public string FlightId { get; set; }

    public BookingDto ToDTO(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.BookingId,
            PassengerId = booking.PassengerId,
            FlightId = booking.FlightId
        };
    }

    
}