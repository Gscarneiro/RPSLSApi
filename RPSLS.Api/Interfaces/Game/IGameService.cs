using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Interfaces
{
    public interface IGameService
    {
        Result GetGameResult(Move playerChoice, Move opponentChoice);
    }
}
