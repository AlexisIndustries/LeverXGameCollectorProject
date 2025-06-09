using FluentValidation.Results;
using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponceModel>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAuthService _authService;

        public RegisterCommandHandler(
            UserManager<UserEntity> userManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<AuthResponceModel> Handle(RegisterCommand request, CancellationToken ct)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.request.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Email already registered");
            }

            var user = new UserEntity
            {
                FirstName = request.request.FirstName,
                LastName = request.request.LastName,
                Email = request.request.Email,
                UserName = request.request.Email,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.request.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException();
            }

            await _userManager.AddToRoleAsync(user, "User");

            return await _authService.GenerateAuthResponse(user);
        }
    }
}
