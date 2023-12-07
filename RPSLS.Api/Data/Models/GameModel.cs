using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;
using System.Text.Json.Serialization;

namespace RPSLS.Api.Data.Models
{
    public class GameModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("roomId")]
        public Guid RoomId { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("playerOneMove")]
        public Move? PlayerOneMove { get; set; }
        [JsonPropertyName("playerTwoMove")]
        public Move? PlayerTwoMove { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }

        public static GameModel FromDTO(Game dto)
        {
            return new GameModel() {
                Id = dto.Id,
                RoomId = dto.RoomId,
                Date = dto.Date,
                Status = dto.Status,
                PlayerOneMove = dto.PlayerOneMove,
                PlayerTwoMove = dto.PlayerTwoMove,
                Result = dto.Result,
            };
        }
    }        
}
