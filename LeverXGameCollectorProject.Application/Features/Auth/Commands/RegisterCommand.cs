using LeverXGameCollectorProject.Application.DTOs.Auth;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Commands
{
    public record RegisterCommand(RegisterRequestModel request) : IRequest<AuthResponceModel>
    {
    }
}
