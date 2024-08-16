using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Models;
using System.Security.Claims;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all user info including bookings and reviews
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserInfo()
        {
            // Get user ID from the token/claims
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userDto = new UserProfile
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                IsAdmin = user.IsAdmin,
                Bookings = user.Bookings.Select(b => new BookingDto
                {
                    Id = b.Id,
                    GroundId = b.GroundId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status
                }).ToList(),
                
            };

            return Ok(userDto);
        }
    }
}
