using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;
using RPSLS.Api.Data.Constants;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Services
{
    public class GameService(RPSLSDbContext context, IGameRepository game) : BaseService(context), IGameService
    {
        private readonly IGameRepository gameRepository = game;

        public Game CreateGame(Guid roomId)
        {
            var game = Game.Create(roomId);

            gameRepository.Insert(game);

            DbContext.SaveChanges();

            return game;
        }

        public void UpdateStatus(Guid roomId, Status status)
        {
            var game = gameRepository.List(roomId).Last();
            
            game.Status = status;

            gameRepository.Update(game);

            DbContext.SaveChanges();
        }

        public bool MakeMove(Guid gameId, Guid playerId, Move move)
        {
            var game = gameRepository.Query()
                .Include(a => a.Room)
                .FirstOrDefault(g => g.Id == gameId);

            if(game.Room.PlayerOneId == playerId) {
                game.PlayerOneMove = move;
            } else if(game.Room.PlayerTwoId == playerId) {
                game.PlayerTwoMove = move;
            } else {
                throw new KeyNotFoundException();
            }

            gameRepository.Update(game);

            DbContext.SaveChanges();

            return game.PlayerOneMove.HasValue && game.PlayerTwoMove.HasValue;
        }


        public (Result playerOneResult, Result playerTwoResult) GetGameResult(Guid gameId)
        {
            var game = gameRepository.GetById(gameId);

            var playerOneResult = GameRules.Rules.Single(gs => gs.Player == game.PlayerOneMove && gs.Opponent == game.PlayerTwoMove).Result;
            var playerTwoResult = GameRules.Rules.Single(gs => gs.Player == game.PlayerTwoMove && gs.Opponent == game.PlayerOneMove).Result;

            game.Result = playerOneResult == Result.Win ? "Player One Wins!" : playerTwoResult == Result.Win ? "Player Two Wins!" : "Draw!";

            game.Status = Status.GameEnded;

            gameRepository.Update(game);

            DbContext.SaveChanges();

            return (playerOneResult, playerTwoResult);
        }
    }
}
