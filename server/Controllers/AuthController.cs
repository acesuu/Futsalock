using Microsoft.AspNetCore.Mvc;
using server.Dto;
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
    public async Task<IActionResult> SignUp([FromBody] UserDto userDto)
    {
        var existingUser = await _userRepository.GetUserAsync(userDto.Username, userDto.Password);
        if (existingUser != null)
        {
            return BadRequest("User already exists.");
        }

        var newUser = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            IsAdmin = userDto.IsAdmin
        };

        await _userRepository.AddUserAsync(newUser, userDto.Password);
        return Ok(newUser);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginDto loginDto)
    {
        var user = await _userRepository.GetUserAsync(loginDto.Username, loginDto.Password);
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
