using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository(DataContext context) : IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons()
        {
            return context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int id)
        {
            return context.Pokemon.Find(id);
        }

        public Pokemon GetPokemon(string name)
        {
            return context.Pokemon.FirstOrDefault(p=>p.Name==name);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review= context.Reviews.Where(r=>r.Id==pokeId);

            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r=>r.Rating)/review.Count());
        }

        public bool PokemonExists(int pokeId)
        {
           return context.Pokemon.Any(p=>p.Id==pokeId);
        }
    }
}
