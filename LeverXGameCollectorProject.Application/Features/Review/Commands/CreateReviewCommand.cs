﻿using LeverXGameCollectorProject.Application.DTOs.Review;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Commands
{
    public record CreateReviewCommand(CreateReviewRequestModel Request) : IRequest<int>;
}
