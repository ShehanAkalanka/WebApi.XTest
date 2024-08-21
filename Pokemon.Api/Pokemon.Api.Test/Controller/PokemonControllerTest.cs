using FakeItEasy;
using FluentAssertions;
using Pokemon.Api.Controllers;
using Pokemon.Api.Interfaces;
using Pokemon.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.Tests
{
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly PokemonController _controller;

        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _controller = new PokemonController(_pokemonRepository);
        }

        [Fact]
        public async Task GetAllPokemons_ReturnsOkResult_WithListOfPokemons()
        {
            // Arrange
            var pokemons = new List<PokemonModel>
            {
                new PokemonModel { Id = 1, Name = "Pikachu", CategoryId = 1 },
                new PokemonModel { Id = 2, Name = "Charmander", CategoryId = 2 }
            };

            A.CallTo(() => _pokemonRepository.GetAllPokemonsAsync()).Returns(Task.FromResult((IEnumerable<PokemonModel>)pokemons));

            // Act
            var result = await _controller.GetAllPokemons();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(pokemons);
        }

        [Fact]
        public async Task GetPokemonById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            int testId = 1;
            A.CallTo(() => _pokemonRepository.GetPokemonByIdAsync(testId)).Returns(Task.FromResult<PokemonModel>(null));

            // Act
            var result = await _controller.GetPokemonById(testId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetPokemonById_ExistingIdPassed_ReturnsOkResult_WithPokemon()
        {
            // Arrange
            var testId = 1;
            var testPokemon = new PokemonModel { Id = testId, Name = "Pikachu", CategoryId = 1 };

            A.CallTo(() => _pokemonRepository.GetPokemonByIdAsync(testId)).Returns(Task.FromResult(testPokemon));

            // Act
            var result = await _controller.GetPokemonById(testId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(testPokemon);
        }

        [Fact]
        public async Task AddPokemon_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var newPokemon = new PokemonModel { Name = "Bulbasaur", CategoryId = 1 };

            // Act
            var result = await _controller.AddPokemon(newPokemon);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.Value.Should().BeEquivalentTo(newPokemon);
        }

        [Fact]
        public async Task DeletePokemon_NotExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var notExistingId = 0;
            A.CallTo(() => _pokemonRepository.DeletePokemonAsync(notExistingId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePokemon(notExistingId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
