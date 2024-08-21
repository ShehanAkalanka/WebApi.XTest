using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Interfaces;
using Pokemon.Api.Model;

namespace Pokemon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonModel>>> GetAllPokemons()
        {
            var pokemons = await _pokemonRepository.GetAllPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonModel>> GetPokemonById(int id)
        {
            var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<ActionResult> AddPokemon([FromBody] PokemonModel pokemon)
        {
            await _pokemonRepository.AddPokemonAsync(pokemon);
            return CreatedAtAction(nameof(GetPokemonById), new { id = pokemon.Id }, pokemon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] PokemonModel pokemon)
        {
            if (id != pokemon.Id)
            {
                return BadRequest();
            }

            await _pokemonRepository.UpdatePokemonAsync(pokemon);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            await _pokemonRepository.DeletePokemonAsync(id);
            return NoContent();
        }
    }
}
