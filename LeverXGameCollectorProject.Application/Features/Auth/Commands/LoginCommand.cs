using LeverXGameCollectorProject.Application.DTOs.Auth;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Commands
{
    public record LoginCommand(LoginRequestModel request) : IRequest<AuthResponceModel>
    {
    }
}
