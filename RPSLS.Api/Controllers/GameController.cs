using Microsoft.AspNetCore.Mvc;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Services;

namespace RPSLS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService game)
        {
            gameService = game;
        }


        [HttpPost("{room}/player1")]
        public IActionResult GetPlayer1Result(string room)
        {
            return null;
        }

        [HttpPost("{room}/player2")]
        public async Task<IActionResult> Get(int room)
        {
            return null;
        }
    }
}
