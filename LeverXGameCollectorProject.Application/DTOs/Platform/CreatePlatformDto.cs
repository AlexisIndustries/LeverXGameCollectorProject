﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.DTOs.Platform
{
    public class CreatePlatformDto
    {
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
    }
}
