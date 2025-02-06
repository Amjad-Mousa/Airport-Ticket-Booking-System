namespace Airport_Ticket_Booking_System.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Booking> Bookings { get; set; }

        public Passenger(int id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Bookings = new List<Booking>();
        }

        public override string ToString()
        {
            return $"Passenger ID: {Id}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}