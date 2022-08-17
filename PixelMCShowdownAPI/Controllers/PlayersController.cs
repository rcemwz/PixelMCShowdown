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
        private readonly AppSettings _appSettings;

        public PlayersController(IPlayerRepository playerRepository, IOptions<AppSettings> appSettings)
        {
            this._playerRepository = playerRepository;
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
            return Ok(await _playerRepository.GetPlayer(id));
        }

        // POST api/<PlayersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<PostMinecraftPlayer> players)
        {
            List<MinecraftPlayer> toReturn = new List<MinecraftPlayer>();

            foreach (var player in players)
            {
                Guid uuid;
                bool successfulParse = Guid.TryParse(player.UUID, out uuid);

                if (!successfulParse)
                    return BadRequest("Invalid UUID: " + player.UUID + " for " + player.PlayerName);

                try
                {
                    Player p = await _playerRepository.AddPlayer(uuid, player.PlayerName);
                    toReturn.Add(new MinecraftPlayer
                    {
                        Rank = 0,
                        Score = p.ELORating,
                        UUID = p.UUID
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            return Ok(toReturn);
        }
    }
}
