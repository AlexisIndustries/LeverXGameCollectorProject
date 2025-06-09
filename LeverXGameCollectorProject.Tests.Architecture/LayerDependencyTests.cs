using LeverXGameCollectorProject.Application.Services;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper;
using LeverXGameCollectorProject.Models;
using NetArchTest.Rules;

namespace LeverXGameCollectorProject.Tests.Architecture
{
    public class LayerDependencyTests
    {
        private readonly Types _domainTypes = Types.InAssembly(typeof(Game).Assembly);
        private readonly Types _applicationTypes = Types.InAssembly(typeof(DeveloperService).Assembly);
        private readonly Types _infrastructureTypes = Types.InAssembly(typeof(DapperDeveloperRepository).Assembly);
        private readonly Types _apiTypes = Types.InAssembly(typeof(Controllers.GamesController).Assembly);

        [Fact]
        public void Domain_Should_Not_Depend_On_Other_Layers()
        {
            var result = _domainTypes
                .ShouldNot()
                .HaveDependencyOnAny(
                    "LeverXGameCollectorProject.Application",
                    "LeverXGameCollectorProject.Infrastructure",
                    "LeverXGameCollectorProject.API")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Or_API()
        {
            var result = _applicationTypes
                .ShouldNot()
                .HaveDependencyOnAny(
                    "LeverXGameCollectorProject.Infrastructure",
                    "LeverXGameCollectorProject.API")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Repositories_Should_Be_Named_Correctly()
        {
            var result = _infrastructureTypes
                .That()
                .ImplementInterface(typeof(Domain.Interfaces.IRepository<>))
                .Should()
                .HaveNameEndingWith("Repository")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UseCases_Should_Not_Depend_On_Infrastructure()
        {
            var result = _applicationTypes
                .That()
                .HaveNameEndingWith("UseCase")
                .ShouldNot()
                .HaveDependencyOn("LeverXGameCollectorProject.Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
