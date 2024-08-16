using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dto;
using server.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroundController : ControllerBase
    {
        private readonly GroundRepository _groundRepository;

        public GroundController(GroundRepository groundRepository)
        {
            _groundRepository = groundRepository;
        }

        // Get all available grounds
        [HttpGet("available-grounds")]
        public async Task<IActionResult> GetAvailableGrounds([FromQuery] DateTime date)
        {
            var availableGrounds = await _groundRepository.GetAvailableGroundsAsync(date);
            return Ok(availableGrounds);
        }

        // Add a ground
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddGround([FromBody] GroundDto groundDto)
        {
            
            Debug.WriteLine("Entered AddGround method");

           
            if (!User.Identity.IsAuthenticated)
            {
                Debug.WriteLine("User is not authenticated");
                return Unauthorized("You are not authenticated");
            }

            
            var name = User.Identity.Name;
            Debug.WriteLine($"Authenticated User Name: {name}");

            var roleClaim = User.FindFirst(ClaimTypes.Role);
            var role = roleClaim?.Value;

            if (roleClaim == null)
            {
                Debug.WriteLine("Role claim not found in the JWT token");
            }
            else
            {
                Debug.WriteLine($"User Role: {role}");
            }

            if (role != "Admin")
            {
                Debug.WriteLine("User does not have the Admin role");
                return Forbid("You do not have permission to perform this action");
            }

            // Retrieve the admin ID from the JWT token claims
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (adminIdClaim == null)
            {
                Debug.WriteLine("Admin ID claim not found");
                return BadRequest("Admin ID claim is missing from the token");
            }

            var adminId = int.Parse(adminIdClaim.Value);
            Debug.WriteLine($"Admin ID: {adminId}");

            
            var ground = new Ground
            {
                Name = groundDto.Name,
                Location = groundDto.Location,
                GoogleMap = groundDto.GoogleMap,
                Description = groundDto.Description,
                HourlyRate = groundDto.HourlyRate,
                IsActive = groundDto.IsActive,
                AdminId = adminId
            };

            try
            {
                var newGround = await _groundRepository.AddGroundAsync(ground, adminId);
                Debug.WriteLine("Ground added successfully");
                return Ok(newGround);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding ground: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the ground");
            }
        }




        // Update an existing ground
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGround([FromBody] UpdateGroundDto groundDto)
        {
            // Get Admin ID from the token/claims
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            try
            {
                // Retrieve the existing ground by ID and ensure it belongs to the admin
                var existingGround = await _groundRepository.GetGroundByIdAsync(groundDto.Id);
                if (existingGround == null || existingGround.AdminId != adminId)
                {
                    return NotFound("Ground not found or you do not have permission to update this ground.");
                }

                
                existingGround.Name = groundDto.Name;
                existingGround.Location = groundDto.Location;
                existingGround.GoogleMap = groundDto.GoogleMap;
                existingGround.Description = groundDto.Description;
                existingGround.HourlyRate = groundDto.HourlyRate;
                existingGround.IsActive = groundDto.IsActive;

                
                var updatedGround = await _groundRepository.UpdateGroundAsync(existingGround, adminId);
                return Ok(updatedGround);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Get all grounds by admin
        [Authorize(Roles = "Admin")]
        [HttpGet("my-grounds")]
        public async Task<IActionResult> GetGroundsByAdmin()
        {
            
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var grounds = await _groundRepository.GetGroundsByAdminAsync(adminId);
            return Ok(grounds);
        }
    }
}
