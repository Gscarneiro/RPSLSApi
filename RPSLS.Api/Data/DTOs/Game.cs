using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Data.DTO
{
    public class Game: BaseDTO
    {
        public Guid RoomId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Move? PlayerOneMove { get; set; }
        public Move? PlayerTwoMove { get; set; }
        public string Result { get; set; }

        public virtual Room Room { get; set; }

        public static Game Create(Guid? roomId = null)
        {
            return new Game() {
                Id = Guid.NewGuid(),
                RoomId = roomId ?? Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Status = Status.WaitingForPlayerToJoin
            };
        }

    }
}
