using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Data.DTO
{
    public class Room : BaseDTO
    {
        public string Name { get; set; }
        public Guid? PlayerOneId { get; set; }
        public Guid? PlayerTwoId { get; set; }
        public bool Public { get; set; }

        public virtual Player PlayerOne { get; set; }
        public virtual Player PlayerTwo { get; set; }
        public ICollection<Game> GamesList { get; set; }

        public static Room Create(string playerName, string roomName, bool publicRoom = true)
        {
            return new Room() {
                Id = Guid.NewGuid(),
                Name = roomName,
                Public = publicRoom,
                PlayerOne = Player.Create(playerName),
                GamesList = [Game.Create()]
            };
        }
    }
}
