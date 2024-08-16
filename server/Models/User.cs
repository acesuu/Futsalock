namespace server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Ground> Grounds { get; set; }

    }
}
