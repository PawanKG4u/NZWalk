using AutoMapper;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Profiles
{
    public class WalkProfile:Profile
    {
        public WalkProfile() 
        {
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyDTO>().ReverseMap();
        }
    }
}
