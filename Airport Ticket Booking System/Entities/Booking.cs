using Airport_Ticket_Booking_System.DTOs;
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
        public object Status { get; set; }

        public Booking(string flightId, int passengerId, FlightClass bookingClass)
        {
            FlightId = flightId;
            PassengerId = passengerId;
            BookingDate = DateTime.Now;
            BookingId = _bookingId++;
            BookingClass = bookingClass;
    
            var flight = FlightService.GetFlightById(flightId);
            if (flight != null)
            {
                TotalPrice = flight.Price + (decimal)bookingClass;
            }
            else
            {
                TotalPrice = 0;
            }

            BookingDto bookingDto = new BookingDto();
            bookingDto.ToDTO(this);
        }


        public override string ToString()
        {
            return
                $"FlightId:{this.FlightId}, FlightID:{this.FlightId}, PassengerId:{this.PassengerId}, BookingDate:{this.BookingDate} ";
        }
    }
}