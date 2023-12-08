using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;
using RPSLS.Api.Interfaces;

namespace RPSLS.Api.Services
{
    public class RoomService(RPSLSDbContext context, IRoomRepository room, IGameService game) : BaseService(context), IRoomService
    {
        private readonly IRoomRepository roomRepository = room;
        private readonly IGameService gameService = game;

        public Room CreateRoom(string playerName, string roomName, bool publicRoom = true)
        {
            var room = Room.Create(playerName, roomName, publicRoom);
            
            roomRepository.Insert(room);

            DbContext.SaveChanges();

            return room;
        }

        public List<Room> List(bool? publicRoom = null, bool showFull = false)
        {
            return roomRepository.List(publicRoom, showFull);
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

            gameService.UpdateStatus(roomId, Status.WaitingForPlayerToJoin);

            roomRepository.Update(room);

            DbContext.SaveChanges();

            return room;
        }

    }
}
