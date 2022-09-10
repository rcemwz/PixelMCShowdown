using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixelMCShowdownAPI.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        public Guid UUID { get; set; }
        public string PlayerName { get; set; }
        public int ELORating { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<BattleStat> BattleStats { get; set; }
        public virtual ICollection<BattleStat> WonBattleStats { get; set; }
    }
}
