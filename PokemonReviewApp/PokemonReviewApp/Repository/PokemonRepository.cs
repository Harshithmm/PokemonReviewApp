using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
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

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity=context.Owners.FirstOrDefault(o => o.Id==ownerId);
            var pokemonCategoryEntity=context.Categories.FirstOrDefault(c => c.Id==categoryId);

            var pokemonOwner = new PokemonOwner
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            context.Add(pokemonOwner);
            var pokemonCategory = new PokemonCategory
            {
                Category = pokemonCategoryEntity,
                Pokemon = pokemon
            };

            context.Add(pokemonCategory);
            context.Add(pokemon);

            return Save();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            context.Update(pokemon);
            return Save();
        }

        public bool Save()
        {
            return context.SaveChanges() > 0;
        }
    }
}
