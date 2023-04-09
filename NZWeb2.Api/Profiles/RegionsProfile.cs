using AutoMapper;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Profiles
{
    public class RegionsProfile:Profile
    {
        public RegionsProfile() 
        {
            CreateMap<Region, RegionDTO>()
                .ReverseMap();
            CreateMap<Region, AddRegionRequestDTO>()
              .ReverseMap();
        }
    }
}
