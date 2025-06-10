using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponceModel>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IAuthService _authService;

        public LoginCommandHandler(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        public async Task<AuthResponceModel> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(request.request.Email);
            if (user == null)
            {
                throw new ApplicationException("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.request.Password, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid credentials");
            }

            return await _authService.GenerateAuthResponse(user);
        }
    }
}
