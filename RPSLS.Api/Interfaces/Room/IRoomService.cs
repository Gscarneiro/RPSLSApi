using RPSLS.Api.Data.Enums;
using RPSLS.Api.Data.DTO;

namespace RPSLS.Api.Interfaces
{
    public interface IRoomService
    {
        Room CreateRoom(string playerName, bool publicRoom = true);
        List<Room> List(bool? publicRoom = null, bool showFull = false);
        Room JoinRoom(Guid roomId, string playerName);
        Room LeaveRoom(Guid roomId, Guid playerId);
    }
}
