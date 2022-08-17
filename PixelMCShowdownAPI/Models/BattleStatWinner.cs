namespace PixelMCShowdownAPI.Models
{
    public class BattleStatWinner
    {
        public int BattleStatId { get; set; }
        public int PlayerId { get; set; }

        public BattleStat BattleStat { get; set; }
        public Player Winner { get; set; }
    }
}
