namespace server.Dto
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }

        public List<BookingDto> Bookings { get; set; }
    }
}
