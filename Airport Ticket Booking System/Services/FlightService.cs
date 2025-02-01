using System.Collections.Immutable;
using Airport_Ticket_Booking_System.DTOs;
using Airport_Ticket_Booking_System.Entities;
using Airport_Ticket_Booking_System.Helper;
namespace Airport_Ticket_Booking_System.Services;
using Airport_Ticket_Booking_System.Entities;
public class FlightService
{
    public  static Flight? GetFlightById(string id)
    {
         return CsvHelperService.ReadFromCsv<Flight>("Flight.csv")
            .FirstOrDefault(f=>f.Id==id);
    }
}
