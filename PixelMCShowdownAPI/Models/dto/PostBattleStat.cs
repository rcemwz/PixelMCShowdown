using PixelMCShowdownAPI.Enum;

namespace PixelMCShowdownAPI.Models.dto
{
    public class PostBattleStat
    {
        public class PostBattleStatParticipant
        {
            public Guid UUID { get; set; }
            public BattleOutcome battleOutcome { get; set; }
        }

        public ICollection<PostBattleStatParticipant> Participants { get; set; }
    }
}
