namespace server.Models
{
    public class BlockedTimeSlot
    {
        public int BlockedTimeSlotId { get; set; }
        public int GroundId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Reason { get; set; } 

        public Ground Ground { get; set; } 
    }
}
