using System.ComponentModel.DataAnnotations;

namespace PixelMCShowdownAPI.Models.dto
{
    public class BattleStatRequest
    {
        [Required]
        public IEnumerable<Guid> Players { get; set; }
        [Required]
        public IEnumerable<Guid> Winners { get; set; }
    }
}
