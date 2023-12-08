using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RPSLS.Api.Data.Models;
using RPSLS.Api.Hubs;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Services;

namespace RPSLS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomService room, IHubContext<GameHub> hub) : ControllerBase
    {
        private readonly IRoomService roomService = room;

        private readonly IHubContext<GameHub> hubContext = hub;

        [HttpPost("")]
        public IActionResult CreateRoom(string roomName, Guid playerId, bool publicRoom = true)
        {
            var room = RoomModel.FromDTO(roomService.CreateRoom(roomName, playerId, publicRoom));

            return Ok(room);
        }

        [HttpGet("list-public")]
        public IActionResult ListPublicRooms(bool showFull = false)
        {
            var room = roomService.List(publicRoom: true, showFull).Select(r => RoomModel.FromDTO(r));

            return Ok(room);
        }

        [HttpPut("join/{roomId}")]
        public async Task<IActionResult> JoinRoom(Guid roomId, Guid playerId)
        {
            var room = RoomModel.FromDTO(roomService.JoinRoom(roomId, playerId));

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
