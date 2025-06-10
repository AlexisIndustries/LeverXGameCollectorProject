using LeverXGameCollectorProject.Application.DTOs.Review;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Features.Review.Queries
{
    public record GetReviewByGameIdQuery(int Id) : IRequest<ReviewResponseModel>;
}
