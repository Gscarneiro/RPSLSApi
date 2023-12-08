using Microsoft.AspNetCore.Mvc;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Models;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Services;

namespace RPSLS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService player)
        {
            playerService = player;
        }


        [HttpPost("")]
        public IActionResult Create(string name)
        {
            var player  = PlayerModel.FromDTO(playerService.CreatePlayer(name));

            return Ok(player);
        }
    }
}
