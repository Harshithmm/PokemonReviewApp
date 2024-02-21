using AutoMapper;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<PokemonReviewApp.Models.Pokemon, PokemonReviewApp.Dto.PokemonDto>();
            CreateMap<PokemonReviewApp.Models.Category, PokemonReviewApp.Dto.CategoryDto>();
            CreateMap<PokemonReviewApp.Models.Country, PokemonReviewApp.Dto.CountryDto>();
            CreateMap<PokemonReviewApp.Models.Owner, PokemonReviewApp.Dto.OwnerDto>();
        }
    }
}
