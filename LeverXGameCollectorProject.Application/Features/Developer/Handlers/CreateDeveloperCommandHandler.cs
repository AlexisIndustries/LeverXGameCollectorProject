﻿using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, int>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public CreateDeveloperCommandHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDeveloperCommand command, CancellationToken ct)
        {
            var id = await _service.CreateDeveloperAsync(command.Request);
            return id;
        }
    }
}
