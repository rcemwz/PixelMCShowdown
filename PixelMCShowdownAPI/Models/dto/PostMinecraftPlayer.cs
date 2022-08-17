using System.ComponentModel.DataAnnotations;

namespace PixelMCShowdownAPI.Models.dto
{
    public class PostMinecraftPlayer
    {
        [Required]
        public string UUID { get; set; }
        [Required]
        public string PlayerName { get; set; }
    }
}
