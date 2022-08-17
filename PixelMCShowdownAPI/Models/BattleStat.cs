using PixelMCShowdownAPI.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixelMCShowdownAPI.Models
{
    public class BattleStat
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public ICollection<Player> Players { get; set; }
        /// <summary>
        /// length 0 if draw
        /// </summary>
        public ICollection<Player> Winners { get; set; }
    }
}
