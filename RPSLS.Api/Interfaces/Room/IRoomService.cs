using RPSLS.Api.Data.Enums;
using RPSLS.Api.Data.DTO;

namespace RPSLS.Api.Interfaces
{
    public interface IRoomService
    {
        Room CreateRoom(string playerName);
        Room JoinRoom(Guid roomId, string playerName);
        Room LeaveRoom(Guid roomId, Guid playerId);
    }
}
