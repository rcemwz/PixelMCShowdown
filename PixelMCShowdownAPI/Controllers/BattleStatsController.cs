using Microsoft.AspNetCore.Mvc;
using PixelMCShowdownAPI.Models.dto;
using PixelMCShowdownAPI.Models;
using PixelMCShowdownAPI.Repositories;
using PixelMCShowdownAPI.Config;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BattleStatsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ICollection<PostBattleStat.PostBattleStatParticipant> battleStatRequest)
        {
            BattleStat? battlestat;
            try
            {
                battlestat = await _battleStatsRepository.PostBattleStat(battleStatRequest);
            } catch (Exception e)
            {
                return NotFound();
            }

            IEnumerable<Player>? players = await _playerRepository.GetPlayers(battleStatRequest.Select(p => p.UUID));

            var eloMatch = new ELO.ELOMatch(K: _appSettings.ELO.KFactor, eloCap: _appSettings.ELO.ChangeCap);
            foreach(var player in players)
            {
                eloMatch.AddPlayer(
                    player.UUID.ToString(), 
                    battleStatRequest.First(x => x.UUID == player.UUID).battleOutcome == Enum.BattleOutcome.WIN ? 1 : 2, 
                    player.ELORating
                );

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
