using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Repositories
{
    public class RoomRepository(RPSLSDbContext context) : BaseRepository<Room>(context), IRoomRepository
    {

        public List<Room> List(bool? publicRoom = null, bool showFull = false)
        {
            var query = Query();
            
            if(publicRoom.HasValue) {
                query = query.Where(x => x.Public == publicRoom);
            }

            if(showFull) {
                query = query.Where(x => x.PlayerOneId.HasValue && x.PlayerTwoId.HasValue);
            } else {
                query = query.Where(x => !x.PlayerOneId.HasValue || !x.PlayerTwoId.HasValue);
            }
            
            return query.ToList();
        }
    }
}
