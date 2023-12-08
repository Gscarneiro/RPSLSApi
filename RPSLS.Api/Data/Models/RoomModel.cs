using RPSLS.Api.Data.DTO;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace RPSLS.Api.Data.Models
{
    public class RoomModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playerOne")]
        public PlayerModel? PlayerOne { get; set; }
        [JsonPropertyName("playerTwo")]
        public PlayerModel? PlayerTwo{ get; set; }
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("playerCount")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("gamesList")]
        public List<GameModel> GamesList { get; set; } = new List<GameModel>();

        public static RoomModel FromDTO(Room dto)
        {
            return new RoomModel() {
                Id = dto.Id,
                Name = dto.Name,
                PlayerOne = PlayerModel.FromDTO(dto.PlayerOne),
                PlayerTwo = PlayerModel.FromDTO(dto.PlayerTwo),
                Public = dto.Public,
                PlayerCount = (dto.PlayerOneId.HasValue ? 1 : 0) + (dto.PlayerTwoId.HasValue ? 1 : 0),
                GamesList = dto.GamesList?.Select(g => GameModel.FromDTO(g)).ToList()
            };
        }
    }
}
