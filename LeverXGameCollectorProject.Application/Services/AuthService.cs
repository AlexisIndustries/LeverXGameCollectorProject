using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeverXGameCollectorProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<JwtSettings> jwtSettings,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthResponseModel> RegisterAsync(RegisterRequestModel request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Email already registered");
            }

            var user = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new AuthResponseModel
            {
                UserId = user.Id,
                Token = await GenerateJwtToken(user)
            };
        }

        public async Task<AuthResponseModel> LoginAsync(LoginRequestModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApplicationException("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid credentials");
            }

            return new AuthResponseModel
            {
                UserId = user.Id,
                Token = await GenerateJwtToken(user)
            };
        }

        public async Task<UserProfileModel> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return new UserProfileModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        private async Task<string> GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("fullName", $"{user.FirstName} {user.LastName}")
        };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponseModel> GenerateAuthResponse(UserEntity user)
        {
            return new AuthResponseModel
            {
                UserId = user.Id,
                Token = await GenerateJwtToken(user)
            };
        }

        public async Task<UserEntity?> Login(LoginRequestModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                throw new ApplicationException("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid credentials");
            }

            return user;
        }

        public async Task<UserEntity> Register(RegisterRequestModel register)
        {
            var existingUser = await _userManager.FindByEmailAsync(register.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Email already registered");
            }

            var user = new UserEntity
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException();
            }

            await _userManager.AddToRoleAsync(user, "User");
            return user;
        }
    }
}
