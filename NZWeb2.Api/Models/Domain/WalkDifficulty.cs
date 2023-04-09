using System.ComponentModel.DataAnnotations;

namespace NZWeb2.Api.Models.Domain
{
    public class WalkDifficulty
    {
        [Key]
        public Guid Id { get; set; } 
        public string Code { get; set; }
    }
}
