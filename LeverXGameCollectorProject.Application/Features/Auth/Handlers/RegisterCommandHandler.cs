using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseModel>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(
            IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponseModel> Handle(RegisterCommand command, CancellationToken ct)
        {
            UserEntity user = await _authService.Register(command.Request);

            return await _authService.GenerateAuthResponse(user);
        }
        
    }
}
