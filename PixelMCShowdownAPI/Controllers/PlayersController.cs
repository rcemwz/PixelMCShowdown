using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PixelMCShowdownAPI.Config;
using PixelMCShowdownAPI.Models;
using PixelMCShowdownAPI.Models.dto;
using PixelMCShowdownAPI.Repositories;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelMCShowdownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IBattleStatsRepository _battleStatsRepository;
        private readonly AppSettings _appSettings;

        public PlayersController(IPlayerRepository playerRepository, IBattleStatsRepository battleStatsRepository, IOptions<AppSettings> appSettings)
        {
            this._playerRepository = playerRepository;
            this._battleStatsRepository = battleStatsRepository;
            this._appSettings = appSettings.Value;
        }

        // GET: api/<PlayersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _playerRepository.GetPlayers());
        }

        // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player is null)
                return NotFound();
            return Ok(player);
        }

        // GET api/Players/{id}/BattleStats
        [HttpGet("{id}/BattleStats")]
        public async Task<IActionResult> GetBattleStats(Guid id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player == null)
                return NotFound();

            var battleStats = await _battleStatsRepository.GetBattleStats(player.UUID);

            var toReturn = battleStats.Select(bs => new
            {
                Id = bs.Id,
                CreatedDateTime = bs.CreatedDateTime,
                Players = bs.Players.Select(p => new { UUID = p.UUID, PlayerName = p.PlayerName }).ToList(),
                Winners = bs.Winners.Select(p => new { UUID = p.UUID, PlayerName = p.PlayerName }).ToList()
            });

            return Ok(toReturn);
        }

        // POST api/<PlayersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<PostMinecraftPlayer> players)
        {
            List<MinecraftPlayer> toReturn = new List<MinecraftPlayer>();

            foreach (var player in players)
            {
                if ((await _playerRepository.GetPlayer(player.UUID)) != null)
                    continue;

                Player p = await _playerRepository.AddPlayer(player.UUID, player.PlayerName);
                toReturn.Add(new MinecraftPlayer
                {
                    Rank = 0,
                    Score = p.ELORating,
                    UUID = p.UUID
                });
            }

            return Ok(toReturn);
        }

        // GET api/<PlayersController>/5
        [HttpGet("Validate/Exists/{id}")]
        public async Task<IActionResult> ValidateUUIDExists(Guid id)
        {
            if (await _playerRepository.PlayerExists(id))
                 return new JsonResult(true);

            return (await _playerRepository.PlayerExists(id)) ? new JsonResult(true) : new JsonResult("UUID " + id + " does not exist in the database.");
        }
    }
}
