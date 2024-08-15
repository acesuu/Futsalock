using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class GroundRepository
    {
        private readonly AppDbContext _context;

        public GroundRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Available Ground List
        public async Task<List<Ground>> GetAvailableGroundsAsync(DateTime date)
        {
            return await _context.Grounds
                .Include(g => g.Bookings)
                .Where(g => g.IsActive)
                .ToListAsync();
        }

        // Add Ground
        public async Task<Ground> AddGroundAsync(Ground ground, int adminId)
        {
            ground.AdminId = adminId; 
            _context.Grounds.Add(ground); 
            await _context.SaveChangesAsync();
            return ground;
        }

        // Update Ground
        public async Task<Ground> UpdateGroundAsync(Ground ground, int adminId)
        {
            
            var existingGround = await _context.Grounds.FirstOrDefaultAsync(g => g.Id == ground.Id && g.AdminId == adminId);
            if (existingGround == null)
            {
                throw new Exception("Ground not found or you do not have permission to update this ground.");
            }

            existingGround.Name = ground.Name;
            existingGround.Location = ground.Location;
            existingGround.HourlyRate = ground.HourlyRate;
            existingGround.IsActive = ground.IsActive;

            await _context.SaveChangesAsync();
            return existingGround;
        }

        // Get Grounds by Admin
        public async Task<List<Ground>> GetGroundsByAdminAsync(int adminId)
        {
            return await _context.Grounds
                .Where(g => g.AdminId == adminId)
                .ToListAsync();
        }

        // Retrieve a ground by ID
        public async Task<Ground> GetGroundByIdAsync(int id)
        {
            return await _context.Grounds.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
