using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Services
{
    public class RoomService(RPSLSDbContext context, IRoomRepository room) : BaseService(context), IRoomService
    {
        private readonly IRoomRepository roomRepository = room;

        public Room CreateRoom(string playerName)
        {
            var room = Room.Create(playerName);
            
            roomRepository.Insert(room);

            DbContext.SaveChanges();

            return room;
        }

        public Room JoinRoom(Guid roomId, string playerName)
        {
            var room = roomRepository.Query().Include(x => x.PlayerOne).Include(x => x.PlayerTwo).Include(x => x.GamesList)
                .FirstOrDefault(r => r.Id == roomId) ?? throw new KeyNotFoundException();

            if(room.PlayerOneId == null) { 
                room.PlayerOne = Player.Create(playerName);
            } else if (room.PlayerTwoId == null) {
                room.PlayerTwo = Player.Create(playerName);
            } else {
                throw new Exception("Room is Full!");
            }

            roomRepository.Update(room);

            DbContext.SaveChanges();

            return room;

        }

        public Room LeaveRoom(Guid roomId, Guid playerId)
        {
            var room = roomRepository.Query().Include(x => x.PlayerOne).Include(x => x.PlayerTwo).Include(x => x.GamesList)
                .FirstOrDefault(r => r.Id == roomId) ?? throw new KeyNotFoundException();
            
            if(room.PlayerOneId == playerId) {
                room.PlayerOneId = null;
            } else if(room.PlayerTwoId == playerId) {
                room.PlayerTwoId = null;
            } else {
                throw new KeyNotFoundException();
            }

            roomRepository.Update(room);

            DbContext.SaveChanges();

            return room;
        }
    }
}
