using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dto;
using server.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;
        private readonly GroundRepository _groundRepository;

        public BookingController(BookingRepository bookingRepository, GroundRepository groundRepository)
        {
            _bookingRepository = bookingRepository;
            _groundRepository = groundRepository;
        }

        // User can create a booking
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Debug.WriteLine("Create booking entered");

            var ground = await _groundRepository.GetGroundByIdAsync(createBookingDto.GroundId);
            if (ground == null)
            {
                Debug.WriteLine("Ground not found.");
                return NotFound("Ground not found.");
            }

            var booking = new Booking
            {
                GroundId = createBookingDto.GroundId,
                UserId = userId,
                StartTime = createBookingDto.StartTime,
                EndTime = createBookingDto.EndTime,
                TotalPrice = createBookingDto.TotalPrice,
                Status = "Pending" // Default status is Pending
            };

            
            var newBooking = await _bookingRepository.CreateBookingAsync(booking);

            Debug.WriteLine("Booking saved");
            return Ok(newBooking);
        }

        // Admin can update booking status
        [Authorize(Roles = "Admin")]
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateBookingStatus([FromBody] UpdateBookingStatusDto updateBookingStatusDto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var booking = await _bookingRepository.GetBookingByIdAsync(updateBookingStatusDto.BookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Only the admin of the ground can update the booking status
            if (booking.Ground.AdminId != adminId)
            {
                return Unauthorized("You do not have permission to update the status of this booking.");
            }

            booking.Status = updateBookingStatusDto.Status; // Update the status
            await _bookingRepository.UpdateBookingAsync(booking);

            return Ok(booking);
        }

        // Admin marks payment as completed, which allows marking the booking as completed
        [Authorize(Roles = "Admin")]
        [HttpPut("mark-payment-completed/{bookingId}")]
        public async Task<IActionResult> MarkPaymentCompleted(int bookingId)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Only the admin of the ground can mark the payment as completed
            if (booking.Ground.AdminId != adminId)
            {
                return Unauthorized("You do not have permission to mark the payment for this booking.");
            }

            booking.IsPaymentCompleted = true;
            booking.Status = "Completed";
            await _bookingRepository.UpdateBookingAsync(booking);

            return Ok(booking);
        }
    }

}
