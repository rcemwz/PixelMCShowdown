using PixelMCShowdownAPI.Database;
using PixelMCShowdownAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static PixelMCShowdownAPI.Models.dto.PostBattleStat;

namespace PixelMCShowdownAPI.Repositories
{
    public class BattleStatsRepository : IBattleStatsRepository
    {
        private readonly PixelMCShowdownDBContext _context;

        public BattleStatsRepository(PixelMCShowdownDBContext pixelMCShowdownDBcontext)
        {
            _context = pixelMCShowdownDBcontext;
        }

        public async Task<IEnumerable<BattleStat>> GetBattleStats(params Guid[] uuids) => _context.BattleStats
                .Include(bs => bs.Winners)
                .Include(bs => bs.Players)
                .AsSplitQuery()
                .Where(bs =>
                    bs.Players.Any(player =>
                        uuids.Any(u =>
                            u.Equals(player.UUID)
                            )
                        )
                    );

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

        public async Task<BattleStat> PostBattleStat(IEnumerable<PostBattleStatParticipant> participants)
        {
            var nullParticipants = participants.Where(player => _context.Players.FirstOrDefault(p => p.UUID == player.UUID) == null);
            if (nullParticipants.Count() > 0)
                throw new Exception("Null player(s): " + String.Join(", ", nullParticipants.Select(p => p.UUID.ToString()).ToArray()));

            var participatingPlayers = participants.Select(p => _context.Players.First(player => player.UUID.Equals(p.UUID)));
            BattleStat battleStat = new BattleStat
            {
                Players = participatingPlayers.ToList(),
                Winners = participants.Where(pp => pp.battleOutcome == Enum.BattleOutcome.WIN).Select(p => _context.Players.First(player => player.UUID.Equals(p.UUID))).ToList(),
                CreatedDateTime = DateTime.Now,
            };

            _context.BattleStats.Add(battleStat);
            await _context.SaveChangesAsync();

            return battleStat;
        }
    }
}
