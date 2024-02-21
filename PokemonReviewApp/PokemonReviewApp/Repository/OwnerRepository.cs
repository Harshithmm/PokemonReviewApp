using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository(DataContext context):IOwnerRepository
    {
        public ICollection<Owner> GetOwners()
        {
            return context.Owners.ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return context.Owners.Find(ownerId);
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(p => p.Owner).ToList();
            return context.PokemonOwners.Where(p=>p.PokemonId==pokeId).Select(p=>p.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return context.PokemonOwners.Where(p=>p.OwnerId==ownerId).Select(p=>p.Pokemon).ToList();
            //return context.PokemonOwners.Where(p=>p.Owner.Id==ownerId).Select(p=>p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return context.Owners.Any(o=>o.Id==ownerId);
        }
    }
}
