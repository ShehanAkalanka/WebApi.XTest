using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Data;
using Pokemon.Api.Interfaces;
using Pokemon.Api.Model;

namespace Pokemon.Api.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PokemonModel>> GetAllPokemonsAsync()
        {
            return await _context.Pokemons.Include(p => p.Category).ToListAsync();
        }

        public async Task<PokemonModel> GetPokemonByIdAsync(int id)
        {
            return await _context.Pokemons.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPokemonAsync(PokemonModel pokemon)
        {
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePokemonAsync(PokemonModel pokemon)
        {
            _context.Pokemons.Update(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
            }
        }
    }
}
