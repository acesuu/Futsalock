namespace server.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int GroundId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
