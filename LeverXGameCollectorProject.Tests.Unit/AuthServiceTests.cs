using LeverXGameCollectorProject.Application;
using LeverXGameCollectorProject.Application.Services;
using LeverXGameCollectorProject.Domain.Entities.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LeverXGameCollectorProject.Tests.Unit
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<UserEntity>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<SignInManager<UserEntity>> _signInManagerMock;
        private readonly JwtSettings _jwtSettings;
        private AuthService _authService;

        public AuthServiceTests()
        {
            // Mock UserManager
            var store = new Mock<IUserStore<UserEntity>>();
            _userManagerMock = new Mock<UserManager<UserEntity>>(
                store.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
            _signInManagerMock = new Mock<SignInManager<UserEntity>>(
                _userManagerMock.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            // Mock RoleManager
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);

            // JWT settings
            _jwtSettings = new JwtSettings
            {
                SecretKey = "super-secret-key-for-testing-at-least-32-characters-long",
                ValidIssuer = "TestIssuer",
                ValidAudience = "TestAudience",
                ExpiryInMinutes = 60
            };

            var options = Options.Create(_jwtSettings);

            _authService = new AuthService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                options,
                _roleManagerMock.Object);
        }

        [Fact]
        public async Task GenerateJwtToken_ShouldReturnValidToken()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = "user-123",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe"
            };

            var roles = new List<string> { "User", "Admin" };

            _userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var token = await _authService.GenerateAuthResponse(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token.Token);

            // Verify token content
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token.Token);

            Assert.Equal(_jwtSettings.ValidIssuer, jsonToken.Issuer);
            Assert.Equal(_jwtSettings.ValidAudience, jsonToken.Audiences.First());

            // Check claims
            Assert.Equal(user.Id, jsonToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Assert.Equal(user.Email, jsonToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
            Assert.Equal($"{user.FirstName} {user.LastName}", jsonToken.Claims.First(c => c.Type == "fullName").Value);

            // Check roles
            var roleClaims = jsonToken.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            Assert.Equal(2, roleClaims.Count);
            Assert.Contains("User", roleClaims.Select(c => c.Value));
            Assert.Contains("Admin", roleClaims.Select(c => c.Value));

            // Check expiration
            var expectedExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);
            var actualExpiry = jsonToken.ValidTo;
            Assert.True(actualExpiry > DateTime.UtcNow && actualExpiry <= expectedExpiry);
        }


        [Fact]
        public async Task GetUserProfile_ShouldReturnCorrectProfile()
        {
            // Arrange
            var userId = "user-789";
            var user = new UserEntity
            {
                Id = userId,
                Email = "profile@test.com",
                FirstName = "Alice",
                LastName = "Johnson"
            };
            var password = "ValidPass123!";
            var roles = new List<string> { "User", "Admin" };

            _userManagerMock.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, password, false))
            .ReturnsAsync(SignInResult.Success);

            _userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var profile = await _authService.GetUserProfileAsync(userId);

            // Assert
            Assert.NotNull(profile);
            Assert.Equal(user.Id, profile.Id);
            Assert.Equal(user.Email, profile.Email);
            Assert.Equal(user.FirstName, profile.FirstName);
            Assert.Equal(user.LastName, profile.LastName);
            Assert.Equal(roles, profile.Roles);
        }

    }
}
