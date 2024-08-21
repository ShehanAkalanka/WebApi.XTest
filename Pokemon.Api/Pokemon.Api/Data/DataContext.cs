using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Model;

namespace Pokemon.Api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<PokemonModel> Pokemons { get; set; }
    }
}
