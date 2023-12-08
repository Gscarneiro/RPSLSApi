using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;
using RPSLS.Api.Data.Constants;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Services
{
    public class PlayerService(RPSLSDbContext context, IPlayerRepository player) : BaseService(context), IPlayerService
    {
        private readonly IPlayerRepository playerRepository = player;

        public Player CreatePlayer(string name)
        {
            var player = Player.Create(name);

            playerRepository.Insert(player);

            DbContext.SaveChanges();

            return player;
        }

       
    }
}
