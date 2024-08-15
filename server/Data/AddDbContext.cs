using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BlockedTimeSlot> BlockedTimeSlots { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User to Booking Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ground to Booking Relationship
            modelBuilder.Entity<Ground>()
                .HasMany(g => g.Bookings)
                .WithOne(b => b.Ground)
                .HasForeignKey(b => b.GroundId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ground to Review Relationship
            modelBuilder.Entity<Ground>()
                .HasMany(g => g.Reviews)
                .WithOne(r => r.Ground)
                .HasForeignKey(r => r.GroundId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Review Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User (Admin) to Ground Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Grounds)
                .WithOne(g => g.Admin)  // Link ground to the admin user
                .HasForeignKey(g => g.AdminId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of users if they have grounds

            // Ground to BlockedTimeSlot Relationship
            modelBuilder.Entity<Ground>()
                .HasMany(g => g.BlockedTimeSlots)
                .WithOne(bts => bts.Ground)
                .HasForeignKey(bts => bts.GroundId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
