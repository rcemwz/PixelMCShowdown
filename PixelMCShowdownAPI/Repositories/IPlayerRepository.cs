using PixelMCShowdownAPI.Models;

namespace PixelMCShowdownAPI.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player?> GetPlayer(Guid uuid);
        Task<IEnumerable<Player>> GetPlayers(IEnumerable<Guid> uuid);
        Task<IEnumerable<Player>> GetPlayers();

        Task<bool> PlayerExists(Guid uuid);

        Task<Player> AddPlayer(Guid uuid, string playerName);
        Task<Player> UpdatePlayer(Player player);
    }
}
