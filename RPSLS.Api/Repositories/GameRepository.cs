using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Repositories
{
    public class GameRepository(RPSLSDbContext context) : BaseRepository<Game>(context), IGameRepository
    {
        public List<Game> List(Guid roomId)
        {
            var query = Query().Where(r => r.RoomId == roomId);

            return query.OrderBy(r => r.Date).ToList();
        }
    }
}
