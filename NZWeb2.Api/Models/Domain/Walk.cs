using System.ComponentModel.DataAnnotations;

namespace NZWeb2.Api.Models.Domain
{
    public class Walk
    {
        //public Walk() {
        //    Regions = new HashSet<Region>();
        //    WalkDifficultys = new HashSet<WalkDifficulty>();
        //}
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        public Region Regions { get; set; }
        public WalkDifficulty WalkDifficultys { get; set; }

        //public ICollection<Region> Regions { get; set; }
        //public ICollection<WalkDifficulty> WalkDifficultys { get; set; }
    }
}
