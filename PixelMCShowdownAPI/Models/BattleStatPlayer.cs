namespace PixelMCShowdownAPI.Models
{
    public class BattleStatPlayer
    {
        public int BattleStatId { get; set; }
        public int PlayerId { get; set; }

        public BattleStat BattleStat { get; set; }
        public Player Player { get; set; }
    }
}
