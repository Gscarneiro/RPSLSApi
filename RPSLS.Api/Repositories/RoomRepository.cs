using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(RPSLSDbContext context) : base(context)
        {
        }

        public Room CreateRoom(string playerName)
        {
            throw new NotImplementedException();
        }

        public Room JoinRoom(Guid room, string playerName)
        {
            throw new NotImplementedException();
        }

        public Room LeaveRoom(Guid room, string playerName)
        {
            throw new NotImplementedException();
        }
    }
}
