using Microsoft.AspNetCore.Mvc;
using PixelMCShowdownAPI.Models.dto;
using PixelMCShowdownAPI.Models;
using PixelMCShowdownAPI.Repositories;
using PixelMCShowdownAPI.Config;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelMCShowdownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleStatsController : ControllerBase
    {
        private readonly IBattleStatsRepository _battleStatsRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly AppSettings _appSettings;

        public BattleStatsController(IBattleStatsRepository battleStatsRepository, IPlayerRepository playerRepository, IOptions<AppSettings> appSettings)
        {
            this._battleStatsRepository = battleStatsRepository;
            this._playerRepository = playerRepository;
            this._appSettings = appSettings.Value;
        }

        // GET: api/<BattleStatsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BattleStatsController>/5
        [HttpGet("{id}")]
        public string Get(Guid playerUuid)
        {
            return "value";
        }

        // POST api/<BattleStatsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BattleStatRequest battleStatRequest)
        {
            BattleStat? battlestat = await _battleStatsRepository.PostBattleStat(battleStatRequest.Players, battleStatRequest.Winners);
            IEnumerable<Player>? players = await _playerRepository.GetPlayers(battleStatRequest.Players);

            var eloMatch = new ELO.ELOMatch(_appSettings.ELO.KFactor);
            foreach(var player in players)
            {
                if (battleStatRequest.Winners.Contains(player.UUID))
                    eloMatch.AddPlayer(player.UUID.ToString(), 1, player.ELORating);
                else
                    eloMatch.AddPlayer(player.UUID.ToString(), 2, player.ELORating);
            }
            eloMatch.CalculateELOs();
            
            foreach(var player in players)
            {
                player.ELORating = eloMatch.GetELO(player.UUID.ToString());
                await _playerRepository.UpdatePlayer(player);
            }

            var responseMcPlayers = players.Select(player => new MinecraftPlayer
            {
                UUID = player.UUID,
                Score = player.ELORating,
                Rank = player.ELORating / _appSettings.ELO.PointsPerRank
            });

            return Ok(new BattleStatResponse
            {
                Players = responseMcPlayers,
            });
        }
    }
}
