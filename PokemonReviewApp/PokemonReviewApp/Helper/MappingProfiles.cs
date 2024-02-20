using AutoMapper;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<PokemonReviewApp.Models.Pokemon, PokemonReviewApp.Dto.PokemonDto>();
        }
    }
}
