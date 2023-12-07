using RPSLS.Api.Data.Constants;
using RPSLS.Api.Data.Enums;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Services
{
    public class GameService : IGameService
    {
        public Result GetGameResult(Move playerChoice, Move opponentChoice)
        {
            return GameRules.Rules.Single(gs => gs.Player == playerChoice && gs.Opponent == opponentChoice).Result;
        }
    }
}
