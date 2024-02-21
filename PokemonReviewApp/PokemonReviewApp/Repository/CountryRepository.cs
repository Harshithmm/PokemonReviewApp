using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository(DataContext context,IMapper mapper):ICountryRepository
    {
        public ICollection<Country> GetCountries()
        {
            return context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return context.Countries.Find(id);
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return context.Owners.Where(o=>o.Id==ownerId).Select(o=>o.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return context.Countries.Where(c => c.Id == countryId).SelectMany(c => c.Owners).ToList();

            //return context.Owners.Where(o=>o.Country.Id==countryId).ToList();
        }

        public bool CountryExists(int id)
        {
            return context.Countries.Any(c=>c.Id==id);
        }
    }
}
