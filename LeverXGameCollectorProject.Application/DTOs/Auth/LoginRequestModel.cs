using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Application.DTOs.Auth
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
