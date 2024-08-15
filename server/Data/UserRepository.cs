using server.Data;
using server.Models;
using Microsoft.EntityFrameworkCore;


public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        // checking the password hash.
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
    }
}
