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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // User to Booking Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            // Ground to Booking Relationship
            modelBuilder.Entity<Ground>()
                .HasMany(g => g.Bookings)
                .WithOne(b => b.Ground)
                .HasForeignKey(b => b.GroundId);

            // Ground to Review Relationship
            modelBuilder.Entity<Ground>()
                .HasMany(g => g.Reviews)
                .WithOne(r => r.Ground)
                .HasForeignKey(r => r.GroundId);

            // User to Review Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }
    }
}