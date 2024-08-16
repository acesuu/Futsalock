namespace server.Dto
{
    public class UpdateGroundDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string GoogleMap { get; set; }
        public string Description { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsActive { get; set; }
    }
}
