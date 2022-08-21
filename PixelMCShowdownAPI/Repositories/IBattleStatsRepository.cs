using PixelMCShowdownAPI.Models;
using static PixelMCShowdownAPI.Models.dto.PostBattleStat;

namespace PixelMCShowdownAPI.Repositories
{
    public interface IBattleStatsRepository
    {
        Task<IEnumerable<BattleStat>> GetBattleStats(params Guid[] uuids);
        Task<BattleStat> PostBattleStat(IEnumerable<Guid> players, IEnumerable<Guid> winners);
        Task<BattleStat> PostBattleStat(IEnumerable<PostBattleStatParticipant> participants);
    }
}
