using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile:Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region,Models.DTO.RegionDTO>()
                .ReverseMap();
            CreateMap<Models.Domain.Region,Models.DTO.AddRegionRequest>()
                .ReverseMap();
            CreateMap<Region,UpdateRegionRequest>()
                .ReverseMap();
        }
    }
}
