using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string validationError = "Validation error, check your username or password";

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="command">User registration data</param>
        /// <response code="200">Returns authentication tokens and user details</response>
        /// <response code="400">If input validation fails or application error occurs</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel command)
        {
            try
            {
                var response = await _mediator.Send(new RegisterCommand(command));
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = validationError });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user and provides access credentials
        /// </summary>
        /// <param name="command">User login credentials</param>
        /// <response code="200">Returns authentication tokens</response>
        /// <response code="401">If invalid credentials are provided</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel command)
        {
            try
            {
                var response = await _mediator.Send(new LoginCommand(command));
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(new { errors = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves authenticated user's profile information
        /// </summary>
        /// <remarks>
        /// Requires valid JWT token in Authorization header
        /// </remarks>
        /// <response code="200">Returns user profile data</response>
        /// <response code="401">If authentication token is missing or invalid</response>
        /// <response code="404">If user profile doesn't exist</response>
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var profile = await _mediator.Send(new GetUserProfileQuery { UserId = userId });
                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
