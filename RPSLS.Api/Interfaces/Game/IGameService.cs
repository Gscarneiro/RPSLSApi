using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Interfaces
{
    public interface IGameService
    {
        Game CreateGame(Guid roomId);

        void UpdateStatus(Guid roomId, Status status);

        bool MakeMove(Guid gameId, Guid playerId, Move move);

        (Result playerOneResult, Result playerTwoResult) GetGameResult(Guid gameId);
    }
}
