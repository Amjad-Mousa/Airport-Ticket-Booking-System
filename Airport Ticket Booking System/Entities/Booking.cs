using Airport_Ticket_Booking_System.Services;
namespace Airport_Ticket_Booking_System.Entities
{
    public class Booking
    {
        private static int _bookingId = 1;
        public string FlightId { get; set; }
        public int PassengerId { get; set; }
        public FlightClass BookingClass { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookingId { get; }

        public Booking(string flightId, int passengerId, FlightClass bookingClass)
        {
            FlightId = flightId;
            PassengerId = passengerId;
            BookingDate = DateTime.Now;
            BookingId = _bookingId++;
            BookingClass = bookingClass;
            
            TotalPrice = FlightService.GetFlightById(flightId)!.Price + (decimal)bookingClass;
        }

      
    }
}