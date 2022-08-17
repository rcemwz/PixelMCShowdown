using PixelMCShowdownAPI.Models;

namespace PixelMCShowdownAPI.Repositories
{
    public interface IBattleStatsRepository
    {
        Task<IEnumerable<BattleStat>> GetBattleStats(params Guid[] uuids);
        Task<BattleStat> PostBattleStat(IEnumerable<Guid> players, IEnumerable<Guid> winners);
    }
}
