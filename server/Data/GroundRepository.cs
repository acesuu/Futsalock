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

        public async Task<List<Ground>> GetAvailableGroundsAsync(DateTime date)
        {
            return await _context.Grounds
                .Include(g => g.Bookings)
                .Where(g => g.IsActive)
                .ToListAsync();
        }
    }
}
