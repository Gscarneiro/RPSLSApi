using Microsoft.AspNetCore.Mvc;
using RPSLS.Api.Data.Models;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Services;

namespace RPSLS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService room)
        {
            roomService = room;
        }


        [HttpPost("")]
        public IActionResult CreateRoom(String playerName)
        {
            var room = RoomModel.FromDTO(roomService.CreateRoom(playerName));

            return Ok(room);
        }

        [HttpPut("join/{roomId}")]
        public async Task<IActionResult> JoinRoom(Guid roomId, String playerName)
        {
            var room = RoomModel.FromDTO(roomService.JoinRoom(roomId, playerName));

            return Ok(room);
        }

        [HttpPut("leave/{roomId}")]
        public async Task<IActionResult> LeaveRoom(Guid roomId, Guid playerId)
        {
            var room = RoomModel.FromDTO(roomService.LeaveRoom(roomId, playerId));

            return Ok(room);
        }
    }
}
