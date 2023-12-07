using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Data.DTO
{
    public class Room : BaseDTO
    {
        public Guid? PlayerOneId { get; set; }
        public Guid? PlayerTwoId { get; set; }
        public bool Public { get; set; }
        public ICollection<Game> GamesList { get; set; }

        public virtual Player PlayerOne { get; set; }
        public virtual Player PlayerTwo { get; set; }

        public static Room Create(string playerName)
        {
            return new Room() {
                Id = Guid.NewGuid(),
                Public = true,
                PlayerOne = Player.Create(playerName),
                GamesList = [Game.Create()]
            };
        }
    }
}
