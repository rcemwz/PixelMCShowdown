using System.ComponentModel.DataAnnotations;

namespace PixelMCShowdownAPI.Models.dto
{
    public class PostMinecraftPlayer
    {
        [Required]
        public Guid UUID { get; set; }
        [Required]
        public string PlayerName { get; set; }
    }
}
