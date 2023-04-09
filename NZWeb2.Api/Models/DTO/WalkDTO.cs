using NZWeb2.Api.Models.Domain;

namespace NZWeb2.Api.Models.DTO
{
    public class WalkDTO
    {
        //public WalkDTO()
        //{
        //    Regions = new HashSet<RegionDTO>();
        //    WalkDifficultys = new HashSet<WalkDifficultyDTO>();
        //}

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        public RegionDTO Regions { get; set; }
        public WalkDifficultyDTO WalkDifficultys { get; set; }

        //public ICollection<RegionDTO> Regions { get; set; }
        //public ICollection<WalkDifficultyDTO> WalkDifficultys { get; set; }
    }
}
