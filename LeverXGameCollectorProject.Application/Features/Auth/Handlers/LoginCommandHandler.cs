using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseModel>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponseModel> Handle(LoginCommand command, CancellationToken ct)
        {
            UserEntity? user = await _authService.Login(command.Request);

            return await _authService.GenerateAuthResponse(user!);
        }

        
    }
}
