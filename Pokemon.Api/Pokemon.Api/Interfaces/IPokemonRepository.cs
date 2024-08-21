using Pokemon.Api.Model;

namespace Pokemon.Api.Interfaces
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<PokemonModel>> GetAllPokemonsAsync();
        Task<PokemonModel> GetPokemonByIdAsync(int id);
        Task AddPokemonAsync(PokemonModel pokemon);
        Task UpdatePokemonAsync(PokemonModel pokemon);
        Task DeletePokemonAsync(int id);
    }
}
