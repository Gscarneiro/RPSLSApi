using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Interfaces
{
    public interface IGameService
    {
        Game CreateGame(Guid roomId);

        void UpdateStatus(Guid roomId, Status status);

        (Status status, string result) MakeMove(Guid gameId, Guid playerId, Move move);
    }
}
