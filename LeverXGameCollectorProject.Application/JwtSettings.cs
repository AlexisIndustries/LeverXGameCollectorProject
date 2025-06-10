namespace LeverXGameCollectorProject.Application
{
    public class JwtSettings
    {
        public string? SecretKey { get; init; }
        public string? ValidIssuer { get; init; }
        public string? ValidAudience { get; init; }
        public double ExpiryInMinutes { get; init; }
    }
}
