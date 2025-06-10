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
                return BadRequest();
            }
        }

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
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("profile")]
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
