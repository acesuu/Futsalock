namespace server.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int GroundId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // [ Confirmed, Pending, Cancelled ]

        public Ground Ground { get; set; } 
        public User User { get; set; }
    }
}
