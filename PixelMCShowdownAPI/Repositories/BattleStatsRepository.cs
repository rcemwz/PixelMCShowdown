using PixelMCShowdownAPI.Database;
using PixelMCShowdownAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PixelMCShowdownAPI.Repositories
{
    public class BattleStatsRepository : IBattleStatsRepository
    {
        private readonly PixelMCShowdownDBContext _context;

        public BattleStatsRepository(PixelMCShowdownDBContext pixelMCShowdownDBcontext)
        {
            _context = pixelMCShowdownDBcontext;
        }

        public async Task<IEnumerable<BattleStat>> GetBattleStats(params Guid[] uuids)
        {
            throw new NotImplementedException();
        }

        public async Task<BattleStat> PostBattleStat(IEnumerable<Guid> players, IEnumerable<Guid> winners)
        { 
            if (players.Any(player => _context.Players.FirstOrDefault(p => p.UUID == player) == null))
                throw new Exception("Null player");

            if (winners.Any(winner => !players.Contains(winner)))
                throw new Exception("Winner did not take part");

            var participatingPlayers = players.Select(p => _context.Players.First(player => player.UUID.Equals(p)));
            BattleStat battleStat = new BattleStat {
                Players = participatingPlayers.ToList(),
                Winners = winners.Select(p => participatingPlayers.First(player => player.UUID.Equals(p))).ToList(),
                CreatedDateTime = DateTime.Now,
            };

            _context.BattleStats.Add(battleStat);
            await _context.SaveChangesAsync();

            return battleStat;
        }
    }
}
