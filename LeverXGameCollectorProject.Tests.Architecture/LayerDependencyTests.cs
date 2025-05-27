using NetArchTest.Rules;
using static System.Net.Mime.MediaTypeNames;

namespace LeverXGameCollectorProject.Tests.Architecture
{
    public class LayerDependencyTests
    {
        private readonly Types _domainTypes = Types.InAssembly(typeof(LeverXGameCollectorProject.Domain.Interfaces.IGameRepository).Assembly);
        private readonly Types _applicationTypes = Types.InAssembly(typeof(LeverXGameCollectorProject.Application.Interfaces.IGameService).Assembly);
        private readonly Types _infrastructureTypes = Types.InAssembly(typeof(LeverXGameCollectorProject.Infrastructure.Persistence.InMemory.InMemoryGameRepository).Assembly);
        private readonly Types _apiTypes = Types.InAssembly(typeof(LeverXGameCollectorProject.Controllers.GamesController).Assembly);

        [Fact]
        public void Domain_Should_Not_Depend_On_Other_Layers()
        {
            var result = _domainTypes
                .ShouldNot()
                .HaveDependencyOnAny(
                    "GameCollection.Application",
                    "GameCollection.Infrastructure",
                    "GameCollection.API")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Or_API()
        {
            var result = _applicationTypes
                .ShouldNot()
                .HaveDependencyOnAny(
                    "GameCollection.Infrastructure",
                    "GameCollection.API")
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
                .HaveDependencyOn("GameCollection.Infrastructure")
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}
