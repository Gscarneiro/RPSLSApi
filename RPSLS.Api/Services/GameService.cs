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
            var game = Game.Create(roomId, Status.GameStarted);

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

        public (Status status, string result) MakeMove(Guid gameId, Guid playerId, Move move)
        {
            var game = gameRepository.Query()
                .Include(x => x.Room).ThenInclude(Room => Room.PlayerOne)
                .Include(x => x.Room).ThenInclude(Room => Room.PlayerTwo)
                .FirstOrDefault(g => g.Id == gameId);

            if(game.Room.PlayerOneId == playerId) {
                game.PlayerOneMove = move;
            } else if(game.Room.PlayerTwoId == playerId) {
                game.PlayerTwoMove = move;
            } else {
                throw new KeyNotFoundException();
            }

            if(game.PlayerOneMove.HasValue && game.PlayerTwoMove.HasValue) {
                GetGameResult(game);
            } else {
                game.Status = Status.WaitingForPlayerToSelect;
            }

            gameRepository.Update(game);

            var status = game.Status;

            var result = game.Result;

            DbContext.SaveChanges();

            return (status, result);
        }


        public string GetGameResult(Game game)
        {
            var playerOneResult = GameRules.Rules.Single(gs => gs.Player == game.PlayerOneMove && gs.Opponent == game.PlayerTwoMove).Result;
            var playerTwoResult = GameRules.Rules.Single(gs => gs.Player == game.PlayerTwoMove && gs.Opponent == game.PlayerOneMove).Result;

            if(playerOneResult == Result.Win) {
                game.Result = $"{game.Room.PlayerOne.Name} Wins!";
            } else if(playerTwoResult == Result.Win) {
                game.Result = $"{game.Room.PlayerOne.Name} Wins!";
            } else {
                game.Result = $"It's a Draw!";
            }

            game.Status = Status.GameEnded;

            return game.Result;
        }
    }
}
