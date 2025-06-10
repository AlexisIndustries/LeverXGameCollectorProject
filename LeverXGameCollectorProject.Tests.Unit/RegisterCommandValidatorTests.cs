using FluentValidation.TestHelper;
using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Commands;
using LeverXGameCollectorProject.Application.Features.Auth.Validators;

namespace LeverXGameCollectorProject.Tests.Unit
{
        public class RegisterCommandValidatorTests
        {
            private readonly RegisterCommandValidator _validator = new();

            [Theory]
            [InlineData(null, false, "Email is required")]
            [InlineData("", false, "Email is required")]
            [InlineData("invalid-email", false, "Invalid email format")]
            [InlineData("valid@example.com", true, null)]
            public void EmailValidation_ShouldBeCorrect(string email, bool isValid, string errorMessage)
            {
                // Arrange
                var command = new RegisterRequestModel {
                    Email = email,
                    Password = "ValidPass123!",
                    FirstName = "John",
                    LastName = "Doe"
                };

                // Act
                var result = _validator.TestValidate(command);

                // Assert
                if (isValid)
                {
                    result.ShouldNotHaveValidationErrorFor(x => x.Email);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(x => x.Email)
                        .WithErrorMessage(errorMessage);
                }
            }

            [Theory]
            [InlineData(null, false, "Password is required")]
            [InlineData("", false, "Password is required")]
            [InlineData("short", false, "Password must be at least 8 characters")]
            [InlineData("nouppercase1!", false, "Password must contain at least one uppercase letter")]
            [InlineData("NOLOWER1!", false, "Password must contain at least one lowercase letter")]
            [InlineData("NoDigit!", false, "Password must contain at least one digit")]
            [InlineData("ValidPass123!", true, null)]
            public void PasswordValidation_ShouldBeCorrect(string password, bool isValid, string errorMessage)
            {
                // Arrange
                var command = new RegisterRequestModel
                {
                    Email = "valid@example.com",
                    Password = password,
                    FirstName = "John",
                    LastName = "Doe"
                };

                // Act
                var result = _validator.TestValidate(command);

                // Assert
                if (isValid)
                {
                    result.ShouldNotHaveValidationErrorFor(x => x.Password);
                }
                else
                {
                    result.ShouldHaveValidationErrorFor(x => x.Password)
                        .WithErrorMessage(errorMessage);
                }
            }

            [Fact]
            public void FullValidCommand_ShouldPassAllValidations()
            {
                // Arrange
                var command = new RegisterRequestModel
                {
                    Email = "test@example.com",
                    Password = "PerfectPass123!",
                    FirstName = "John",
                    LastName = "Doe"
                };

                // Act
                var result = _validator.TestValidate(command);

                // Assert
                result.ShouldNotHaveAnyValidationErrors();
            }
        }
    }
