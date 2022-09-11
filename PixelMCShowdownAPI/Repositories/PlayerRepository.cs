using PixelMCShowdownAPI.Database;
using PixelMCShowdownAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PixelMCShowdownAPI.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PixelMCShowdownDBContext _context;

        public PlayerRepository(PixelMCShowdownDBContext pixelMCShowdownDBcontext)
        {
            _context = pixelMCShowdownDBcontext;
        }

        public async Task<Player> AddPlayer(Guid uuid, string playerName)
        {
            _context.Players.Add(new Player
            {
                UUID = uuid,
                PlayerName = playerName,
                ELORating = 0,
                CreatedDateTime = DateTime.Now,
            });
            await _context.SaveChangesAsync();
            return await GetPlayer(uuid);
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return await GetPlayer(player.UUID);
        }

        public async Task<Player?> GetPlayer(Guid uuid) => _context.Players.FirstOrDefault(p => p.UUID == uuid);

        public async Task<IEnumerable<Player>> GetPlayers(IEnumerable<Guid> uuids) => _context.Players.Where(p => uuids.Contains(p.UUID)).ToList();
        public async Task<IEnumerable<Player>> GetPlayers() => _context.Players.OrderBy(p => p.CreatedDateTime);

        public async Task<bool> PlayerExists(Guid uuid) => _context.Players.Any(p => p.UUID.Equals(uuid));
    }
}
