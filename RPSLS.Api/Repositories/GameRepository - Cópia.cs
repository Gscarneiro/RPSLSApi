using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Repositories
{
    public class PlayerRepository(RPSLSDbContext context) : BaseRepository<Player>(context), IPlayerRepository
    {
    }
}
