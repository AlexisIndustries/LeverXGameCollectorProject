using Microsoft.AspNetCore.Identity;

namespace LeverXGameCollectorProject.Domain.Entities.DB
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
