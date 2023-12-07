using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Data.DTO
{
    public class Player : BaseDTO
    {
        public string Name { get; set; }

        public ICollection<Room> PlayerOneRoomsList { get; set; }
        public ICollection<Room> PlayerTwoRoomsList { get; set; }

        public static Player Create(string playerName)
        {
            return new Player() {
                Id = Guid.NewGuid(),
                Name = playerName
            };
        }
    }
}