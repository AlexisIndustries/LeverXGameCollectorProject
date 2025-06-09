namespace LeverXGameCollectorProject.Application.DTOs.Auth
{
    public class UserProfileModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
