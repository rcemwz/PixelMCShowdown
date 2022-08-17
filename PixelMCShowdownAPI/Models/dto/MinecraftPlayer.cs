using System.ComponentModel.DataAnnotations;

namespace PixelMCShowdownAPI.Models.dto
{
    public class MinecraftPlayer
    {
        [Required]
        public Guid UUID { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
    }
}
