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

            DbContext.SaveChanges();

            return (game.Status, game.Result);
        }


        public string GetGameResult(Game game)
        {
            var result = GameRules.Rules.Single(gs => gs.Player == game.PlayerOneMove && gs.Opponent == game.PlayerTwoMove).Result;
            
            if(result == Result.Draw) {
                game.Result = $"It's a Draw!";
            } else if(result == Result.Win) {
                game.Result = $"{game.Room.PlayerOne.Name} Wins!";
            } else {
                game.Result = $"{game.Room.PlayerTwo.Name} Wins!";
            }

            game.Status = Status.GameEnded;

            return game.Result;
        }
    }
}
