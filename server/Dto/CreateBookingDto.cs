namespace server.Dto
{
    public class CreateBookingDto
    {
        public int GroundId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
