using server.Data;
using server.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User user, string password)
    {
        user.PasswordHash = HashPassword(password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null && VerifyPasswordHash(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Ensure consistent UTF-8 encoding
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        using (var sha256 = SHA256.Create())
        {
            // Hash the incoming password again and compare it with the stored hash
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHash = Convert.ToBase64String(bytes);
            return computedHash == storedHash;
        }
    }
}
