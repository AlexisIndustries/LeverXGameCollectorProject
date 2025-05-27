using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.DTOs.Developer
{
    public class UpdateDeveloperDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public DateTime Founded { get; set; }
    }
}
