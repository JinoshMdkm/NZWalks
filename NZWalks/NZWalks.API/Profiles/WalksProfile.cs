using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalksProfile:Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.WalkDTO>()
                .ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDTO>()
                    .ReverseMap();
            CreateMap<AddWalkRequest,Walk>()
                .ReverseMap();
            CreateMap<UpdateWalkRequest,Walk> ()
                .ReverseMap();
        }
    }
}
