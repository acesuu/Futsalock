using Microsoft.AspNetCore.Mvc;
using server.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(UserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] User user)
    {
        var existingUser = await _userRepository.GetUserAsync(user.Username, user.PasswordHash);
        if (existingUser != null)
        {
            return BadRequest("User already exists.");
        }

        var newUser = await _userRepository.AddUserAsync(user);
        return Ok(newUser);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] User loginDetails)
    {
        var user = await _userRepository.GetUserAsync(loginDetails.Username, loginDetails.PasswordHash);
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = JwtUtils.GenerateJwtToken(user, _configuration["JwtKey"]);
        return Ok(new { Token = token });
    }

    // It is handled in the client-side by removing the JWT from storage.
    [HttpPost("signout")]
    public IActionResult SignOut()
    {
        return Ok("Signed out successfully.");
    }
}
