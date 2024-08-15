namespace server.Models
{
    public class Ground
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsActive { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<BlockedTimeSlot> BlockedTimeSlots { get; set; }

        public List<Review> Reviews { get; set; }
        public int AdminId { get; set; } // Foreign key
        public User Admin { get; set; } 

    }
}
