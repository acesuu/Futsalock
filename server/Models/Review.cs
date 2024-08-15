namespace server.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int GroundId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public Ground Ground { get; set; } 
        public User User { get; set; } 
    }
}
